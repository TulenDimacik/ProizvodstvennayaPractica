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
    /// Логика взаимодействия для ProjectChange.xaml
    /// </summary>
    public partial class ProjectChange : Page
    {
        MySqlConnection connect = new MySqlConnection("server=localhost;user=root;database=Lotus;password=;");
        SignIn si = Application.Current.Windows.OfType<SignIn>().FirstOrDefault();
        string loginn;
        string ID;
        string IDProject;
        /// <summary>
        /// Метод для инициализации и присвоения переменных, открытия соединения с базой данных
        /// </summary>
        /// <param name="sign">Переменная хранящая в себе ссылку на основное окно</param>
        /// <param name="IDpr">Переменная, хранящая в себе ID проекта</param>
        /// <param name="login">Переменная хранящая в себе логин сотрудника</param>
        public ProjectChange(SignIn sign, string login, string IDpr)
        {
            InitializeComponent();
            connect.Open();
            IDProject = IDpr;
            si = sign;
            loginn = login;
            MySqlCommand command = new MySqlCommand($@"select  Project_name from Project where ID_Project = {Convert.ToInt32(IDProject)} ", connect);
            MySqlDataReader dataReader = null;
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                tbLogin.Text = dataReader[$@"Project_name"].ToString();
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
            if (e.Source == btnBack)
            {
                si.MainFrame.NavigationService.Navigate(new Projects(si, loginn));
                connect.Close();
            }
            if (e.Source == btnChange)
            {
                if (tbLogin.Text != "")
                {

                    MySqlCommand command = new MySqlCommand($@"call Project_Update({Convert.ToInt32(IDProject)},'{tbLogin.Text}',false)", connect);
                    command.ExecuteNonQuery();
                    si.MainFrame.NavigationService.Navigate(new Projects(si, loginn));
                    connect.Close();
                }
                else
                    MessageBox.Show("Заполните данные!");

            }
        }
    }
}