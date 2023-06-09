﻿using System;
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
            //считаем количество наименований
            countIng.Text = $"{context.Ingredient.Count()} наименований";
            //считаем общую стоимость ингредиентов, которые есть у пользователя
            sumIng.Text = $"Запасов в холодильнике на сумму (руб.) {Math.Round(context.Ingredient.ToList().Sum(x => (x.AvailableCount / x.CostForCount) * Convert.ToDouble(x.Cost)), 2)}";
            ingredientData.ItemsSource = context.Ingredient.ToList();
        }

        /// <summary>
        /// Нажатие на кнопку + (добавить ингредиент)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddIngredientClick(object sender, RoutedEventArgs e)
        {
            //вызываем страницу Добавление ингредиента
            NavigationService.Navigate(new AddIngredientPage(context));
        }


        /// <summary>
        /// Нажатие на гиперссылку "Удалить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveClick(object sender, RoutedEventArgs e)
        {
            //Запрос пользователю, точно ли он хочет удалить
            MessageBoxResult result = MessageBox.Show("Вы уточно хотите удалить ингредиент?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes) //если ответ ДА
            {
                try //если в этом блоке произойдет исключение, программа не вылетит, а перейдет в блок catch
                {
                    //получаем ингредиент, по которому лкикнули "Удалить"
                    Ingredient ing = (sender as Hyperlink).DataContext as Ingredient;
                    //удаляем
                    context.Ingredient.Remove(ing);
                    //сохраняем изменения
                    context.SaveChanges();
                    //снова выводим список в таблицу
                    ingredientData.ItemsSource = context.Ingredient.ToList();
                }
                catch
                {
                    MessageBox.Show("Ошибка!");
                }
            }
        }
        /// <summary>
        /// клик по надписи "Редактировать"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            //получаем ингредиент для редактирования
            Ingredient ing = (sender as Hyperlink).DataContext as Ingredient;
            //вызываем форму "Добавить ингредиент", но передаем в нее объект для редактирования
            //NavigationService.Navigate(new AddIngredientPage(context, ing));
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            Ingredient ing = ingredientData.SelectedItem as Ingredient;
        }
    }
}
