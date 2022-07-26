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
    /// Логика взаимодействия для Notes.xaml
    /// </summary>
    public partial class Notes : Page
    {
        string NoteID;
        string loginn;
        MySqlConnection connect = new MySqlConnection("server=localhost;user=root;database=Lotus;password=;");
        SignIn si = Application.Current.Windows.OfType<SignIn>().FirstOrDefault();
        /// <summary>
        /// Метод для инициализации и присвоения переменных, открытия соединения с базой данных
        /// </summary>
        /// <param name="sign">Переменная хранящая в себе ссылку на основное окно</param>
        /// <param name="login">Переменная хранящая в себе логин сотрудника</param>
        public Notes(SignIn sign, string login)
        {
            InitializeComponent();
            si = sign;
            loginn = login;
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
                si.MainFrame.NavigationService.Navigate(new NoteAdd(si, loginn));
                connect.Close();
            }
            if (e.Source == btnChange)
            {
                if(lbDB.SelectedItem != null)
                {
                    si.MainFrame.NavigationService.Navigate(new NotesChange(si, loginn, NoteID));
                    connect.Close();
                }
                else
                {
                    MessageBox.Show("Выберите заметку");
                }
               
            }
            if (e.Source == btnDelete)
            {
               
                try
                { 
                    MySqlCommand command = new MySqlCommand($@"select ID_Note from Note where Note_name='{lbDB.SelectedItem}'", connect);
                    MySqlDataReader dataReader = null;
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        NoteID = dataReader[$@"ID_Note"].ToString();
                    }
                    dataReader.Close();
                    string com = $@"call Note_Delete ({Convert.ToInt32(NoteID)})";
                    command = new MySqlCommand(com, connect);
                    command.ExecuteNonQuery();
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
                    MySqlCommand command = new MySqlCommand($@"select ID_Note, Note_description from Note where Note_name='{lbDB.SelectedItem}' ", connect);
                    MySqlDataReader dataReader = null;
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        NoteID = dataReader[$@"ID_Note"].ToString();
                        Description.AppendText(dataReader[$@"Note_description"].ToString());
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
            MySqlCommand command = new MySqlCommand($@"select Note_name from Note join Employee where Employee_ID = ID_Employee and Login = '{loginn}' ", connect);
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
