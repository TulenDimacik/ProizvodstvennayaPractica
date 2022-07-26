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
    /// Логика взаимодействия для NoteAdd.xaml
    /// </summary>
    public partial class NoteAdd : Page
    {
        MySqlConnection connect = new MySqlConnection("server=localhost;user=root;database=Lotus;password=;");
        string loginn;
        string IDEmployee;
        SignIn si = Application.Current.Windows.OfType<SignIn>().FirstOrDefault();
        /// <summary>
        /// Метод для инициализации и присвоения переменных, открытия соединения с базой данных
        /// </summary>
        /// <param name="sign">Переменная хранящая в себе ссылку на основное окно</param>
        /// <param name="login">Переменная хранящая в себе логин сотрудника</param>
        public NoteAdd(SignIn sign, string login)
        {
            InitializeComponent();
            loginn = login;
            si = sign;
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
                si.MainFrame.NavigationService.Navigate(new Notes(si, loginn));
                connect.Close();
            }
            if (e.Source == btnAddNote)
            {
                connect.Open();
                try
                {
                    TextRange textRange = new TextRange(Description.Document.ContentStart, Description.Document.ContentEnd);
                    if (tbLogin.Text != "")
                    {
                        MySqlCommand command = new MySqlCommand($@"select ID_Employee from Employee where Login = '{loginn}'", connect);
                        MySqlDataReader dataReader = null;
                        dataReader = command.ExecuteReader();
                        while (dataReader.Read())
                        {
                            IDEmployee = dataReader[$@"ID_Employee"].ToString();
                        }
                        dataReader.Close();
                        command = new MySqlCommand($@"call Note_Insert('{tbLogin.Text}','{textRange.Text}',{Convert.ToInt32(IDEmployee)})", connect);
                        command.ExecuteNonQuery();
                        si.MainFrame.NavigationService.Navigate(new Notes(si, loginn));
                        connect.Close();
                    }
                    else { MessageBox.Show("Заполните данные!"); }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
    }
}
