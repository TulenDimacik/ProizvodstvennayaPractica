using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
    /// Логика взаимодействия для Recovery.xaml
    /// </summary>
    public partial class Recovery : Page
    {
        SignIn si = Application.Current.Windows.OfType<SignIn>().FirstOrDefault();
        int rand;
        MySqlConnection connect = new MySqlConnection("server=localhost;user=root;database=Lotus;password=;");
        /// <summary>
        /// Метод для инициализации и присвоения переменных, открытия соединения с базой данных
        /// </summary>
        /// <param name="sign">Переменная хранящая в себе ссылку на основное окно</param>
        public Recovery(SignIn sign)
        {
            InitializeComponent();
            si = sign;
            Random r = new Random();
            rand = r.Next(100000, 999999);
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
            if (e.Source == btnSendCode)
            {
                try
                {
                    // отправитель - устанавливаем адрес и отображаемое в письме имя
                    MailAddress from = new MailAddress("isip_d.a.gordyushin@mpt.ru", "Lotus");
                    // кому отправляем
                    MailAddress to = new MailAddress(Mail.Text);
                    // создаем объект сообщения
                    MailMessage m = new MailMessage(from, to);
                    // тема письма
                    m.Subject = "Восстановление пароля";

                    m.IsBodyHtml = false;
                    // текст письма
                    m.Body = Convert.ToString(rand);
                    // письмо представляет код html
                    m.IsBodyHtml = true;
                    // адрес smtp-сервера и порт, с которого будем отправлять письмо
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    // логин и пароль
                    smtp.Credentials = new NetworkCredential("isip_d.a.gordyushin@mpt.ru", "Go13rd09");
                    smtp.EnableSsl = true;
                    smtp.Send(m);
                    MessageBox.Show("Код для восстановления пароля был отправлен");
                    btnSendCode.Visibility = Visibility.Hidden;
                    btnConfirm.Visibility = Visibility.Visible;
                    Code.Visibility = Visibility.Visible;
                    Mail.Visibility = Visibility.Hidden;
                    lab.Content = "Код";
                }
                catch
                {
                    MessageBox.Show("Неверный формат почты");
                }
              
            }
            if (e.Source == btnConfirm)
            {
                if(rand.ToString()==Code.Text)
                {
                    connect.Open();
                    MySqlCommand command = new MySqlCommand($"select Passwordd from Employee where Employee_email = '{Mail.Text}' ", connect);
                    MySqlDataReader dataReader = null;
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        MessageBox.Show("Ваш пароль: "+ dataReader[$@"Passwordd"].ToString());
                    }
                    dataReader.Close();
                    connect.Close();
                }
            }
        }
    }
}
