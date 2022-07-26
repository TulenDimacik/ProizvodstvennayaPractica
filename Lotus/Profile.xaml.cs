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
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    /// 
    public partial class Profile : Page
    {
        SignIn si = Application.Current.Windows.OfType<SignIn>().FirstOrDefault();
        string ID;
        string PostID;

        MySqlConnection connect = new MySqlConnection("server=localhost;user=root;database=Lotus;password=;");
        /// <summary>
        /// Метод для инициализации и присвоения переменных, открытия соединения с базой данных
        /// </summary>
        /// <param name="sign">Переменная хранящая в себе ссылку на основное окно</param>
        /// <param name="login">Переменная хранящая в себе логин сотрудника</param>
        public Profile(SignIn sign, string login)
        {
            InitializeComponent();
            si = sign;
            connect.Open();
            MySqlCommand command = new MySqlCommand($@"select ID_Employee, Employee_Surname, Employee_name, Employee_patronymic, Employee_email,Login,Passwordd,Post_ID from Employee where Login = '{login}'", connect);
            MySqlDataReader dataReader = null;
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {   
                ID = dataReader[$@"ID_Employee"].ToString();
                tbName.Text = dataReader[$@"Employee_name"].ToString();
                tbSurname.Text = dataReader[$@"Employee_Surname"].ToString();
                tbPatronymic.Text = dataReader[$@"Employee_patronymic"].ToString();
                tbEmail.Text = dataReader[$@"Employee_email"].ToString();
                tbLogin.Text = dataReader[$@"Login"].ToString();
                tbPassword.Text = dataReader[$@"Passwordd"].ToString();
                PostID = dataReader[$@"Post_ID"].ToString();
            }
            dataReader.Close();
            connect.Close();
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
            if (e.Source == btnChange)
            {
                connect.Open();
                try
                {

                        if (tbSurname.Text != "" && tbName.Text != "" && tbLogin.Text != "" && tbPassword.Text != "" && tbEmail.Text != "")
                        {
                            if (tbPassword.Text.Length >= 8)
                            {
                                string com = $@"call Employee_Update ({Convert.ToInt32(ID)},'{tbName.Text}','{tbSurname.Text}','{tbPatronymic.Text}','{tbEmail.Text}', '{tbLogin.Text}', '{tbPassword.Text}',{Convert.ToInt32(PostID)})";
                                MySqlCommand command = new MySqlCommand(com, connect);
                                command.ExecuteNonQuery();
                                MessageBox.Show("Данные успешно изменены!");
                            }
                            else
                            MessageBox.Show("Слишком короткий пароль!");

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
