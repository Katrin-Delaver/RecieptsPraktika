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
using System.Windows.Shapes;

namespace Reciepts.Pages
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        RecieptsDBEntities context;
        public Registration(RecieptsDBEntities cont)
        {
            InitializeComponent();
            context = cont;

        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            User user = new User()
            {
                Email = emailBox.Text,
                Password = passwordlBox.Text,
                age = Convert.ToInt32(ageBox.Text),
                DateOfRegistration = DateTime.Now,
                DateOfLastLog = DateTime.Now
            };
            context.User.Add(user);
            context.SaveChanges();
            this.Close();

        }
    }
}
