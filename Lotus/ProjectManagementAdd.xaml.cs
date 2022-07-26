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
    /// Логика взаимодействия для ProjectManagementAdd.xaml
    /// </summary>
    public partial class ProjectManagementAdd : Page
    {
        string projectID;
        string IDTask;
        string loginn;
        SignIn si = Application.Current.Windows.OfType<SignIn>().FirstOrDefault();
        MySqlConnection connect = new MySqlConnection("server=localhost;user=root;database=Lotus;password=;");
        /// <summary>
        /// Метод для инициализации и присвоения переменных, открытия соединения с базой данных
        /// </summary>
        /// <param name="sign">Переменная хранящая в себе ссылку на основное окно</param>
        /// <param name="project">Переменная, хранящая в себе ID проекта</param>
        /// <param name="login">Переменная хранящая в себе логин сотрудника</param>
        public ProjectManagementAdd(SignIn sign, string project, string login)
        {
            InitializeComponent();
            si = sign;
            projectID = project;
            loginn = login;
            connect.Open();
            deadline.DisplayDateStart = DateTime.Today;
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
                Application.Current.Windows.OfType<SignIn>().FirstOrDefault().MainFrame.NavigationService.GoBack();
                connect.Close();

            }
            if (e.Source == btnAdd)
            {
                if (name.Text != "")
                {
                    
                    DateTime dateTime = (DateTime)deadline.SelectedDate;
                    TextRange textRange = new TextRange(Description.Document.ContentStart, Description.Document.ContentEnd);
                    MySqlCommand command = new MySqlCommand($@"call Task_Insert('{name.Text}','{dateTime.ToString("yyyy-MM-dd")}','{textRange.Text}',{cbStatus.IsChecked})", connect);
                    command.ExecuteNonQuery();
                    command = new MySqlCommand($@"select MAX(ID_Task) from Task", connect);
                    MySqlDataReader dataReader = null;
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        IDTask = dataReader[$@"MAX(ID_Task)"].ToString();
                    }
                    dataReader.Close();
                    command = new MySqlCommand($@"call Project_tasks_Insert({Convert.ToInt32(projectID)},{Convert.ToInt32(IDTask)})", connect);
                    command.ExecuteNonQuery();
                    connect.Close();
                    si.MainFrame.NavigationService.Navigate(new ProjectManagement(si, projectID, loginn));
                }
                else
                    MessageBox.Show("Заполните данные!");
            }

        }
    }
}
