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
    /// Логика взаимодействия для Authorise.xaml
    /// </summary>
    public partial class Authorise : Page
    {

        MySqlConnection connect = new MySqlConnection("server=localhost;user=root;database=Lotus;password=;");
        SignIn si = Application.Current.Windows.OfType<SignIn>().FirstOrDefault();
        /// <summary>
        /// Метод для инициализации и присвоения переменных
        /// </summary>
        /// <param name="sign">Переменная хранящая в себе ссылку на основное окно</param>
        public Authorise(SignIn sign)
        {
            InitializeComponent();
            si = sign;
        }
        /// <summary>
        /// Метод, который обрабатывает нажатия кнопок и производит необходимые действия в зависимости от нажатой кнопки
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            if (e.Source == btnSignIn)
            {
                connect.Open();
                MySqlCommand command = new MySqlCommand($"select count(*) from Employee where Login = '{tbLogin.Text}' and Passwordd = '{tbPassword.Password}'", connect);
                if (command.ExecuteScalar().ToString() == "1")
                {
                        si.MainFrame.NavigationService.Navigate(new Choose(si, tbLogin.Text));
                }
                else
                    MessageBox.Show("Проверьте введённые данные!");
                connect.Close();
            }
            if (e.Source == btnRegistration)
            {
                si.MainFrame.NavigationService.Navigate(new Registration(si));
            }
            if (e.Source == btnpswdRecovery)
            {
                si.MainFrame.NavigationService.Navigate(new Recovery(si));
            }
        }
    }
}
