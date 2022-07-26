﻿using MySql.Data.MySqlClient;
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
    /// Логика взаимодействия для NotesChange.xaml
    /// </summary>
    public partial class NotesChange : Page
    {
        MySqlConnection connect = new MySqlConnection("server=localhost;user=root;database=Lotus;password=;");
        SignIn si = Application.Current.Windows.OfType<SignIn>().FirstOrDefault();
        string loginn;
        string IDEmployee;
        string IDNote;
        string noteDes;
        /// <summary>
        /// Метод для инициализации и присвоения переменных, открытия соединения с базой данных
        /// </summary>
        /// <param name="sign">Переменная хранящая в себе ссылку на основное окно</param>
        /// <param name="ID">Переменная, хранящая в себе ID проекта</param>
        /// <param name="login">Переменная хранящая в себе логин сотрудника</param>
        public NotesChange(SignIn sign, string login, string ID)
        {
            InitializeComponent();
            loginn = login;
            IDNote = ID;
            si = sign;
            connect.Open();
            MySqlCommand command = new MySqlCommand($@"select Note_name, Note_description from Note where ID_Note = {Convert.ToInt32(ID)}", connect);
            MySqlDataReader dataReader = null;
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                tbLogin.Text = dataReader[$@"Note_name"].ToString();
                Description.AppendText(dataReader[$@"Note_description"].ToString());
            }
            dataReader.Close();
        }
        /// <summary>
        /// Метод, который обрабатывает нажатия кнопок и производит необходимые действия в зависимости от нажатой кнопки
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            if(e.Source == btnBack)
            {
                si.MainFrame.NavigationService.Navigate(new Notes(si, loginn));
                connect.Close();
            }
            if (e.Source == btnChangeNote)
            {
              
                try
                {
                    if (tbLogin.Text != "")
                    {
                        TextRange textRange = new TextRange(Description.Document.ContentStart, Description.Document.ContentEnd);
                        MySqlCommand command = new MySqlCommand($@"select Note_description, Employee_ID from Note join Employee on Employee_ID = ID_Employee where ID_Note = {Convert.ToInt32(IDNote)}", connect);
                        MySqlDataReader dataReader = null;
                        dataReader = command.ExecuteReader();
                        while (dataReader.Read())
                        {
                            noteDes = dataReader[$@"Note_description"].ToString();
                            IDEmployee = dataReader[$@"Employee_ID"].ToString();
                        }
                        dataReader.Close();
                        command = new MySqlCommand($@"call Note_Update({Convert.ToInt32(IDNote)},'{tbLogin.Text}','{textRange.Text}',{Convert.ToInt32(IDEmployee)})", connect);
                        command.ExecuteNonQuery();
                        connect.Close();
                        si.MainFrame.NavigationService.Navigate(new Notes(si, loginn));
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
