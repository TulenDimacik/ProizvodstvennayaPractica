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
    /// Логика взаимодействия для Users.xaml
    /// </summary>
    public partial class Users : Page
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
        public Users(SignIn sign, string ID, string login)
        {
            InitializeComponent();
            si = sign;
            loginn = login;
            connect.Open();
            MySqlCommand command = new MySqlCommand($@"select concat(Employee_Surname,' ',Employee_name,' ',Employee_patronymic) from Employee ", connect);
            MySqlDataReader dataReader = null;
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                lbDB.Items.Add(dataReader.GetValue(0).ToString());
            }
            dataReader.Close();
            connect.Close();
            ProjectID = ID;
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
                si.MainFrame.NavigationService.Navigate(new ProjectUsers(si, ProjectID,loginn));

            }
            if (e.Source == btnAdd)
            {
                connect.Open();
                try
                {
                    if (lbDB.SelectedItem !=null)
                    {
                        string text = lbDB.SelectedItem.ToString();
                        string[] words = text.Split(new char[] { ' ' });
                        MySqlCommand command = new MySqlCommand($@"select ID_Employee from Employee where Employee_Surname='{words[0]}' and Employee_name = '{words[1]}' and  Employee_patronymic='{words[2]}'", connect);
                        MySqlDataReader dataReader = null;
                        dataReader = command.ExecuteReader();
                        while (dataReader.Read())
                        {
                            EmployeeID = dataReader[$@"ID_Employee"].ToString();
                        }
                        dataReader.Close();
                         command = new MySqlCommand($@"call Employee_Projects_Insert({Convert.ToInt32(EmployeeID)},{Convert.ToInt32(ProjectID)})", connect);
                         command.ExecuteNonQuery();
                        MessageBox.Show("Пользователь успешно добавлен");
                    }
                    else { MessageBox.Show("Выберите пользователя"); }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connect.Close();
                }
            }
           
        }
    }
}
