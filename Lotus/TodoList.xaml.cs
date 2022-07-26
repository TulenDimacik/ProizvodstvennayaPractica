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
    /// Логика взаимодействия для TodoList.xaml
    /// </summary>
    public partial class TodoList : Page
    {
        string loginn;
        string todoID;
        string IDEmployee;
        MySqlConnection connect = new MySqlConnection("server=localhost;user=root;database=Lotus;password=;");
        SignIn si = Application.Current.Windows.OfType<SignIn>().FirstOrDefault();
        /// <summary>
        /// Метод для инициализации и присвоения переменных, открытия соединения с базой данных
        /// </summary>
        /// <param name="sign">Переменная хранящая в себе ссылку на основное окно</param>
        /// <param name="login">Переменная хранящая в себе логин сотрудника</param>
        public TodoList(SignIn sign,string login)
        {
            InitializeComponent();
            loginn = login;
            si = sign;
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
                si.MainFrame.NavigationService.Navigate(new Choose(si, loginn));
                connect.Close();
            }
            if (e.Source == btnAdd)
            {
                si.MainFrame.NavigationService.Navigate(new TodoListAdd(si, loginn));
            }
            if (e.Source == btnChange)
            {
                try
                {
                    if (name.Text != "")
                    {
                        DateTime dateTime = (DateTime)deadline.SelectedDate;
                        TextRange textRange = new TextRange(Description.Document.ContentStart, Description.Document.ContentEnd);
                        MySqlCommand command = new MySqlCommand($@"select Employee_ID from Todo_list join Employee on Employee_ID = ID_Employee where ID_Todo_list = {Convert.ToInt32(todoID)}", connect);
                        MySqlDataReader dataReader = null;
                        dataReader = command.ExecuteReader();
                        while (dataReader.Read())
                        {
                            IDEmployee = dataReader[$@"Employee_ID"].ToString();
                        }
                        dataReader.Close();
                        command = new MySqlCommand($@"call Todo_list_Update({Convert.ToInt32(todoID)},'{name.Text}','{dateTime.ToString("yyyy-MM-dd")}','{textRange.Text}',{cbStatus.IsChecked},{Convert.ToInt32(IDEmployee)})", connect);
                        command.ExecuteNonQuery();
                    }
                    else { MessageBox.Show("Заполните данные!"); }
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
            if (e.Source == btnDelete)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand($@"select ID_Todo_list from Todo_list where Case_name='{lbDB.SelectedItem}'", connect);
                    MySqlDataReader dataReader = null;
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        todoID = dataReader[$@"ID_Todo_list"].ToString();
                    }
                    dataReader.Close();
                    string com = $@"call Todo_list_Delete ({Convert.ToInt32(todoID)})";
                    command = new MySqlCommand(com, connect);
                    command.ExecuteNonQuery();
                    Description.Document.Blocks.Clear();
                    name.Text = "";
                    cbStatus.IsChecked = false;
                    deadline.Text = "";
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
            if (e.Source == lbDB)
            {
                
                Description.Document.Blocks.Clear();
                MySqlCommand command = new MySqlCommand($@"select ID_Todo_list, Case_name, Case_deadline, Case_description, Case_Status from Todo_list where Case_name='{lbDB.SelectedItem}' ", connect);
                MySqlDataReader dataReader = null;
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    todoID = dataReader[$@"ID_Todo_list"].ToString();
                    Description.AppendText(dataReader[$@"Case_description"].ToString());
                    name.Text = dataReader[$@"Case_name"].ToString();
                    cbStatus.IsChecked = Convert.ToBoolean(dataReader[$@"Case_Status"].ToString());
                    deadline.Text = dataReader[$@"Case_deadline"].ToString();
                }
                dataReader.Close();

            }
        }
        /// <summary>
        /// Метод для обновления данных
        /// </summary>
        public void Refresh()
        {
          
            lbDB.Items.Clear();
            MySqlCommand command = new MySqlCommand($@"select Case_name from Todo_list join Employee where Employee_ID = ID_Employee and Login = '{loginn}' ", connect);
            MySqlDataReader dataReader = null;
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                lbDB.Items.Add(dataReader.GetValue(0).ToString());
            }
            dataReader.Close();
            
        }
    }
}
