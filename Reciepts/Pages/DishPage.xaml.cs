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
    /// Логика взаимодействия для DishPage.xaml
    /// </summary>
    public partial class DishPage : Page
    {
        RecieptsDBEntities context;
        public DishPage(RecieptsDBEntities cont)
        {
            InitializeComponent();
            context = cont;
            listDishes.ItemsSource = cont.Dish.ToList();

            var categoryList = context.Category.ToList();
            categoryList.Insert(0, new Category() { Name = "Все категории" });
            categoryBox.ItemsSource = categoryList;
            categoryBox.SelectedIndex = 0;
        }

        void FilterData()
        {
            var list = context.Dish.ToList();
            if (categoryBox.SelectedIndex != 0)
            {
                Category category = categoryBox.SelectedItem as Category;
                list = list.Where(x => x.CategoryId == category.Id).ToList();
            }
            if(! string.IsNullOrWhiteSpace(searchBox.Text))
            {
                list = list.Where(x => x.Name.Contains(searchBox.Text)).ToList();
            }

            listDishes.ItemsSource = list;

        }


        private void ChaneCategory(object sender, SelectionChangedEventArgs e)
        {
            FilterData();
        }

        private void SearchChange(object sender, TextChangedEventArgs e)
        {
            FilterData();
        }
    }
}
