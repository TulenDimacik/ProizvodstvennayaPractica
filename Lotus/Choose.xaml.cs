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
    /// Логика взаимодействия для Choose.xaml
    /// </summary>
    public partial class Choose : Page
    {
        string loginn;
        SignIn si = Application.Current.Windows.OfType<SignIn>().FirstOrDefault();
        /// <summary>
        /// Метод для инициализации и присвоения переменных
        /// </summary>
        /// <param name="sign">Переменная хранящая в себе ссылку на основное окно</param>
        /// <param name="login">Переменная хранящая в себе логин сотрудника</param>
        public Choose(SignIn sign, string login)
        {
            InitializeComponent();
            si = sign;
            loginn = login;
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
                si.MainFrame.NavigationService.Navigate(new Authorise(si));
            }
            if (e.Source == btnGoToNote)
            {
                si.MainFrame.NavigationService.Navigate(new Notes(si, loginn));
            }
            if (e.Source == btnGoToTask)
            {
                si.MainFrame.NavigationService.Navigate(new TodoList(si, loginn));
            }
            if (e.Source == btnGoToProject)
            {
                si.MainFrame.NavigationService.Navigate(new Projects(si, loginn));
            }
            if (e.Source == btnGoToProfile)
            {
                si.MainFrame.NavigationService.Navigate(new Profile(si,loginn));
            }
        }
    }
}
