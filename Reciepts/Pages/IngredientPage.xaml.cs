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
    /// Логика взаимодействия для IngredientPage.xaml
    /// </summary>
    public partial class IngredientPage : Page
    {
        RecieptsDBEntities context;
        public IngredientPage(RecieptsDBEntities _cont)
        {
            InitializeComponent();
            context = _cont;
            countIng.Text = $"{context.Ingredient.Count()} наименований";
            sumIng.Text = $"Запасов в холодильнике на сумму (руб.) {Math.Round(context.Ingredient.ToList().Sum(x => (x.AvailableCount / x.CostForCount) * Convert.ToDouble(x.Cost)), 2)}";
            ingredientData.ItemsSource = context.Ingredient.ToList();
        }

        private void AddIngredientClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddIngredientPage(context));
        }

        private void RemoveClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уточно хотите удалить ингредиент?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Ingredient ing = (sender as Hyperlink).DataContext as Ingredient;
                    context.Ingredient.Remove(ing);
                    context.SaveChanges();
                    ingredientData.ItemsSource = context.Ingredient.ToList();
                }
                catch
                {
                    MessageBox.Show("Ошибка!");
                }
            }
        }

        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            Ingredient ing = (sender as Hyperlink).DataContext as Ingredient;
            NavigationService.Navigate(new AddIngredientPage(context, ing));
        }
    }
}
