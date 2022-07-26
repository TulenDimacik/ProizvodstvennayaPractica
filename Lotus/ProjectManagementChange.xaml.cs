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
    /// Логика взаимодействия для ProjectManagementChange.xaml
    /// </summary>
    public partial class ProjectManagementChange : Page
    {
        SignIn si = Application.Current.Windows.OfType<SignIn>().FirstOrDefault();
        string IDTaskk;
        string loginn;
        string projectID;
        MySqlConnection connect = new MySqlConnection("server=localhost;user=root;database=Lotus;password=;");
        /// <summary>
        /// Метод для инициализации и присвоения переменных, открытия соединения с базой данных
        /// </summary>
        /// <param name="sign">Переменная хранящая в себе ссылку на основное окно</param>
        /// <param name="IDTask">Переменная, хранящая в себе ID задачи</param>
        /// <param name="login">Переменная хранящая в себе логин сотрудника</param>
        /// <param name="prID">Переменная хранящая в себе ID проекта</param>
        public ProjectManagementChange(SignIn sign, string IDTask, string login, string prID)
        {
            InitializeComponent();
            connect.Open();
            si = sign;
            IDTaskk = IDTask;
            loginn = login;
            projectID = prID;
            Description.Document.Blocks.Clear();
            MySqlCommand command = new MySqlCommand($@"select  Task_name, Task_deadline, Task_description, Task_Status from Task where ID_Task = {Convert.ToInt32(IDTask)} ", connect);
            MySqlDataReader dataReader = null;
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Description.AppendText(dataReader[$@"Task_description"].ToString());
                name.Text = dataReader[$@"Task_name"].ToString();
                cbStatus.IsChecked = Convert.ToBoolean(dataReader[$@"Task_Status"].ToString());
                deadline.Text = dataReader[$@"Task_deadline"].ToString();
            }
            dataReader.Close();
             command = new MySqlCommand($"select Post_name from Post join Employee on post_ID = ID_post where Login ='{login}'", connect);
            if (command.ExecuteScalar().ToString().Contains("рабочий"))
            {
                header.Content = "Просмотр задачи";
                name.IsEnabled = false;
                cbStatus.IsEnabled = false;
                deadline.IsEnabled = false;
                Description.IsEnabled = false;
                btnChange.Visibility = Visibility.Hidden;
            }

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
            if (e.Source == btnChange)
            {
                DateTime dateTime = (DateTime)deadline.SelectedDate;
                TextRange textRange = new TextRange(Description.Document.ContentStart, Description.Document.ContentEnd);
                MySqlCommand command = new MySqlCommand($@"call Task_Update({Convert.ToInt32(IDTaskk)},'{name.Text}','{dateTime.ToString("yyyy-MM-dd")}','{textRange.Text}',{cbStatus.IsChecked})", connect);
                command.ExecuteNonQuery();
                si.MainFrame.NavigationService.Navigate(new ProjectManagement(si, projectID, loginn));
            }

        }
    }
}
