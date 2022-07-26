using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lotus
{
    /// <summary>
    /// Логика взаимодействия для ProjectUsers.xaml
    /// </summary>
    public partial class ProjectUsers : Page
    {
        SignIn si = Application.Current.Windows.OfType<SignIn>().FirstOrDefault();
        MySqlConnection connect = new MySqlConnection("server=localhost;user=root;database=Lotus;password=;");
        string ProjectID;
        string EmployeeID;
        string loginn;
        /// <summary>
        /// Метод для инициализации и присвоения переменных, открытия соединения с базой данных
        /// </summary>
        /// <param name="sign">Переменная хранящая в себе ссылку на основное окно</param>
        /// <param name="ID">Переменная, хранящая в себе ID проекта</param>
        /// <param name="login">Переменная хранящая в себе логин сотрудника</param>
        public ProjectUsers(SignIn sign, string ID, string login)
        {
            InitializeComponent();
            si = sign;
            ProjectID = ID;
            loginn = login;
            connect.Open();
            Refresh();
        }
        /// <summary>
        /// Метод, который обрабатывает нажатия кнопок и производит необходимые действия в зависимости от нажатой кнопки
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            if (e.Source == btnBack)
            {
                si.MainFrame.NavigationService.Navigate(new ProjectManagement(si, ProjectID, loginn));

            }
            if (e.Source == btnAdd)
            {
                si.MainFrame.NavigationService.Navigate(new Users(si,ProjectID,loginn));
            }
            if (e.Source == btnDelete)
            {
                connect.Open();
                try
                {
                    string text = lbDB.SelectedItem.ToString();
                    string[] words = text.Split(new char[] { ' ' });
                    MySqlCommand command = new MySqlCommand($@"select ID_Employee_Projects from Employee_Projects join Employee on Employee_ID = ID_Employee join Project on Project_ID = ID_Project where Employee_Surname='{words[0]}' and Employee_name = '{words[1]}' and  Employee_patronymic='{words[2]}' and ID_project = {Convert.ToInt32(ProjectID)}", connect);
                    MySqlDataReader dataReader = null;
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        EmployeeID = dataReader[$@"ID_Employee_Projects"].ToString();
                    }
                    dataReader.Close();
                    string com = $@"call Employee_Projects_Delete ({Convert.ToInt32(EmployeeID)})";
                    command = new MySqlCommand(com, connect);
                    command.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Refresh();
                }
            }
        }
        /// <summary>
        /// Метод для обновления данных
        /// </summary>
        public void Refresh()
        {
            lbDB.Items.Clear();   
            MySqlCommand command = new MySqlCommand($@"select concat(Employee_Surname,' ',Employee_name,' ',Employee_patronymic) from Employee_projects join Employee on Employee_ID = ID_Employee join Project on Project_ID = ID_Project where ID_Project ={Convert.ToInt32(ProjectID)}", connect);
            MySqlDataReader dataReader = null;
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                lbDB.Items.Add(dataReader.GetValue(0).ToString());
            }
            dataReader.Close();
            connect.Close();
        }
    }
}
