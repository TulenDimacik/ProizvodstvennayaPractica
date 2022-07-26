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
    /// Логика взаимодействия для ProjectManagement.xaml
    /// </summary>
    public partial class ProjectManagement : Page
    {
        string projectID;
        string TaskID;
        string IDTask;
        string loginn;
        SignIn si = Application.Current.Windows.OfType<SignIn>().FirstOrDefault();
        MySqlConnection connect = new MySqlConnection("server=localhost;user=root;database=Lotus;password=;");
        /// <summary>
        /// Метод для инициализации и присвоения переменных, открытия соединения с базой данных
        /// </summary>
        /// <param name="sign">Переменная хранящая в себе ссылку на основное окно</param>
        /// <param name="ID">Переменная, хранящая в себе ID проекта</param>
        /// <param name="login">Переменная хранящая в себе логин сотрудника</param>
        public ProjectManagement(SignIn sign, string ID, string login)
        {
            InitializeComponent();
            si = sign;
            projectID = ID;
            loginn = login;
            connect.Open();
            MySqlCommand command = new MySqlCommand($"select Post_name from Post join Employee on post_ID = ID_post where Login ='{login}'", connect);
            if (command.ExecuteScalar().ToString().Contains("рабочий"))
            {
                btnUsers.Visibility = Visibility.Hidden;
                btnAdd.Visibility = Visibility.Hidden;
                btnDelete.Visibility = Visibility.Hidden;
                btnChange.Content = "Просмотр";
            }
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
                si.MainFrame.NavigationService.Navigate(new Projects(si, loginn));
            }
            if (e.Source == btnAdd)
            {
                
                si.MainFrame.NavigationService.Navigate(new ProjectManagementAdd(si,projectID, loginn));
                
            }
            if (e.Source == btnChange)
            {
                if(lbDone.SelectedItem != null || lbTodo.SelectedItem !=null)
                {
                    
                    si.MainFrame.NavigationService.Navigate(new ProjectManagementChange(si,IDTask, loginn, projectID));
                }
                else
                {
                    MessageBox.Show("Выберите запись");
                }
                    
            }
            if (e.Source == btnDelete)
            {
                if (lbDone.SelectedItem != null || lbTodo.SelectedItem != null)
                {

                    MySqlCommand command = new MySqlCommand($@"select ID_Project_tasks from Project_tasks join  Task on Task_ID = ID_Task where Task_name='{lbTodo.SelectedItem}' or Task_name='{lbDone.SelectedItem}'", connect);
                    MySqlDataReader dataReader = null;
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        TaskID = dataReader[$@"ID_Project_tasks"].ToString();

                    }
                    dataReader.Close();
                    string com = $@"call Project_tasks_Delete ({Convert.ToInt32(TaskID)})";
                    command = new MySqlCommand(com, connect);
                    command.ExecuteNonQuery();

                    Refresh();
                }
                else
                {
                    MessageBox.Show("Выберите запись");
                }


            }
            if(e.Source == lbDone || e.Source == lbTodo)
            {
             
                MySqlCommand command = new MySqlCommand($@"select ID_Task from Task where Task_name='{lbTodo.SelectedItem}' or Task_name='{lbDone.SelectedItem}'", connect);
                MySqlDataReader dataReader = null;
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    IDTask = dataReader[$@"ID_Task"].ToString();
                }
                dataReader.Close();

            }
            if (e.Source == btnUsers)
            {
                si.MainFrame.NavigationService.Navigate(new ProjectUsers(si, projectID, loginn));
            }
        }
        /// <summary>
        /// Метод для обновления данных
        /// </summary>
        public void Refresh()
        {

            lbDone.Items.Clear();
            lbTodo.Items.Clear();
            MySqlCommand command = new MySqlCommand($@"select Task_name from Project_tasks join Project on Project_ID = ID_Project join Task on Task_ID = ID_Task where Task_status = false and ID_Project = {Convert.ToInt32(projectID)} ", connect);
            MySqlDataReader dataReader = null;
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                lbTodo.Items.Add(dataReader.GetValue(0).ToString());
            }
            dataReader.Close();
            dataReader = null;
            command = new MySqlCommand($@"select Task_name from Project_tasks join  Project on Project_ID = ID_Project join Task on Task_ID = ID_Task where Task_status = true and ID_Project = {Convert.ToInt32(projectID)} ", connect);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                lbDone.Items.Add(dataReader.GetValue(0).ToString());
            }
            dataReader.Close();

        }
    }
}
