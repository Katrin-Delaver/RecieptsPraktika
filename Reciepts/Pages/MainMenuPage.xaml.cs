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
    /// Логика взаимодействия для MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        Window Window;
        RecieptsDBEntities _context;
        public MainMenuPage(RecieptsDBEntities context, Window window)
        {
            InitializeComponent();
            Window = window;
            _context = context;
        }

        private void EscapeClick(object sender, RoutedEventArgs e)
        {
            Window.Close();
        }

        private void IngredientClick(object sender, RoutedEventArgs e)
        {
            frameToBasePages.Navigate(new IngredientPage(_context));
        }
    }
}
