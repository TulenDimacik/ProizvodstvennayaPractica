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
    /// Логика взаимодействия для Projects.xaml
    /// </summary>
    public partial class Projects : Page
    {
        MySqlConnection connect = new MySqlConnection("server=localhost;user=root;database=Lotus;password=;");
        SignIn si = Application.Current.Windows.OfType<SignIn>().FirstOrDefault();
        string ID;
        string idpr;
        string loginn;
        /// <summary>
        /// Метод для инициализации и присвоения переменных, открытия соединения с базой данных
        /// </summary>
        /// <param name="sign">Переменная хранящая в себе ссылку на основное окно</param>
        /// <param name="login">Переменная хранящая в себе логин сотрудника</param>
        public Projects(SignIn sign, string login)
        {
            InitializeComponent();
            si = sign;
            connect.Open();
            
            MySqlCommand command = new MySqlCommand($@"select Project_name from Employee_projects join Project on Project_ID = ID_Project join Employee where Employee_ID = ID_Employee and Login = '{login}' ", connect);
            MySqlDataReader dataReader = null;
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                lbDB.Items.Add(dataReader.GetValue(0).ToString());
            }
            dataReader.Close();
            command = new MySqlCommand($"select Post_name from Post join Employee on post_ID = ID_post where Login ='{login}'", connect);
            if (command.ExecuteScalar().ToString().Contains("рабочий"))
            {
                Project.Visibility = Visibility.Hidden;
                ProjectChange.Visibility = Visibility.Hidden;
            }
            
           
            loginn = login;
           
            
        }
        /// <summary>
        /// Метод, который обрабатывает нажатия кнопок и производит необходимые действия в зависимости от нажатой кнопки
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            if(e.Source == Project)
            {
                si.MainFrame.NavigationService.Navigate(new ProjectAdd(si, loginn));
                connect.Close();
            }
               
            if (e.Source == btnBack)
            {
                si.MainFrame.NavigationService.Navigate(new Choose(si, loginn));
                connect.Close();
            }
            if(e.Source == ProjectChange)
            {
                si.MainFrame.NavigationService.Navigate(new ProjectChange(si, loginn,idpr));
                connect.Close();
            }
            if(e.Source == lbDB)
            {
                MySqlCommand command = new MySqlCommand($@"select ID_Project from Project where Project_name = '{lbDB.SelectedItem}'", connect);
                MySqlDataReader dataReader = null;
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    idpr = dataReader[$@"ID_Project"].ToString();
                }
                dataReader.Close();
            }

            if (e.Source == btnChoose)
            {
                if (lbDB.SelectedItem != null)
                {
                    MySqlCommand command = new MySqlCommand($@"select ID_Project from Project where Project_name = '{lbDB.SelectedItem}'", connect);
                    MySqlDataReader dataReader = null;
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        ID = dataReader[$@"ID_Project"].ToString();
                    }
                    dataReader.Close();
                    connect.Close();
                    si.MainFrame.NavigationService.Navigate(new ProjectManagement(si, ID, loginn));
                }
                else
                    MessageBox.Show("Выберите проект!");
               
            }
        }

        
    }
}
