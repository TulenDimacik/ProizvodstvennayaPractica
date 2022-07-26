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
    /// Логика взаимодействия для TodoListAdd.xaml
    /// </summary>
    public partial class TodoListAdd : Page
    {
        string loginn;
        SignIn si = Application.Current.Windows.OfType<SignIn>().FirstOrDefault();

        MySqlConnection connect = new MySqlConnection("server=localhost;user=root;database=Lotus;password=;");
        string IDEmployee;
        /// <summary>
        /// Метод для инициализации и присвоения переменных, открытия соединения с базой данных
        /// </summary>
        /// <param name="sign">Переменная хранящая в себе ссылку на основное окно</param>
        /// <param name="login">Переменная хранящая в себе логин сотрудника</param>
        public TodoListAdd(SignIn sign, string login)
        {
            InitializeComponent();
            si = sign;
            loginn = login;
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

            }
            if (e.Source == btnAdd)
            {
                connect.Open();
                try
                {
                    if (name.Text != "" )
                    {
                        MySqlCommand command = new MySqlCommand($@"select Employee_ID from Todo_list join Employee on Employee_ID = ID_Employee where Login='{loginn}'", connect);
                        MySqlDataReader dataReader = null;
                        dataReader = command.ExecuteReader();
                        while (dataReader.Read())
                        {
                            IDEmployee = dataReader[$@"Employee_ID"].ToString();
                        }
                        dataReader.Close();
                        
                        DateTime dateTime = (DateTime)deadline.SelectedDate;
                        TextRange textRange = new TextRange(Description.Document.ContentStart, Description.Document.ContentEnd);
                        command = new MySqlCommand($@"call Todo_list_Insert('{name.Text}','{dateTime.ToString("yyyy-MM-dd")}','{textRange.Text}',{cbStatus.IsChecked},{Convert.ToInt32(IDEmployee)})", connect);
                        command.ExecuteNonQuery();
                        si.MainFrame.NavigationService.Navigate(new TodoList(si, loginn));
                    }
                    else { MessageBox.Show("Заполните данные!"); }
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
