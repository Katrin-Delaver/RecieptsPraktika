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

namespace Reciepts.Pages
{
    /// <summary>
    /// Логика взаимодействия для RememberPassPage.xaml
    /// </summary>
    public partial class RememberPassPage : Page
    {
        User _user;
        public RememberPassPage(User user)
        {
            InitializeComponent();
            loginBox.Text = user.Email;
            _user = user;
        }

        private void ShowPassword(object sender, RoutedEventArgs e)
        {
            if (_user.Email == loginBox.Text && _user.age == Convert.ToInt32(ageBox.Text))
                MessageBox.Show($"Ваш пароль: {_user.Password}", "Пароль", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
