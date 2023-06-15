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
        //так как мы находимся на странице, мы не можем обратиться сразу к окну
        //поэтому его получаем из предыдущей страницы
        Window Window;
        RecieptsDBEntities _context;
        public MainMenuPage(RecieptsDBEntities context, Window window)
        {
            InitializeComponent();
            Window = window;
            _context = context;
        }
        /// <summary>
        /// Клик по кнопке "Выход"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EscapeClick(object sender, RoutedEventArgs e)
        {
            //закрытие окна
            Window.Close();
        }
        /// <summary>
        /// открытие страницы Ингредиенты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void IngredientClick(object sender, RoutedEventArgs e)
        {
            frameToBasePages.Navigate(new IngredientPage(_context));
        }

        private void DishClick(object sender, RoutedEventArgs e)
        {
            frameToBasePages.Navigate(new DishPage(_context));
        }
    }
}
