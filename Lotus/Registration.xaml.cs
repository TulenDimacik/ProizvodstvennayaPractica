using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        MySqlConnection connect = new MySqlConnection("server=localhost;user=root;database=Lotus;password=;");
        SignIn si = Application.Current.Windows.OfType<SignIn>().FirstOrDefault();
        /// <summary>
        /// Метод для инициализации и присвоения переменных, открытия соединения с базой данных
        /// </summary>
        /// <param name="sign">Переменная хранящая в себе ссылку на основное окно</param>
        public Registration(SignIn sign)
        {
            InitializeComponent();
            si = sign;
            connect.Open();
            DataTable datatbl1 = new DataTable();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter($@"select ID_Post, Post_name from Post", connect);
            mySqlDataAdapter.Fill(datatbl1);
            cbPost.DisplayMemberPath = "Post_name";
            cbPost.SelectedValuePath = "ID_Post";
            cbPost.ItemsSource = datatbl1.DefaultView;
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
            if (e.Source == btnRegistration)
            {
                connect.Open();
                try
                {
                    if (tbSurname.Text != "" && tbName.Text != "" && tbLogin.Text != ""  && tbEmail.Text!="" && cbPost.SelectedIndex != -1)
                    {
                        if (tbPassword.Text.Length >= 8)
                        {
                            MySqlCommand command = new MySqlCommand($@"call Employee_Insert('{tbSurname.Text}','{tbName.Text}','{tbPatronymic.Text}','{tbEmail.Text}',
                                                                     '{tbLogin.Text}', '{tbPassword.Text}', '{cbPost.SelectedValue}')", connect);
                            command.ExecuteNonQuery();
                            si.MainFrame.NavigationService.Navigate(new Authorise(si));
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
