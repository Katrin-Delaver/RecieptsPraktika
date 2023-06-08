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
    /// Так как для добавления и редактирования один и тот же интерфейс,
    /// можно создать одну страницу для двух действий
    /// Но будут различаться конструкторы: один для добавления, второй для редактирвоания
    /// для кнопки также будет создано 2 разных события. 
    /// Первое для добавления привязывается через интерфейс (.xaml)
    /// Второе для редактирования привязывается в соответствующем конструкторе
    /// </summary>
    public partial class AddIngredientPage : Page
    {
        RecieptsDBEntities context;
        Ingredient ingredient;
        /// <summary>
        /// конструктор для Добавления элемента
        /// </summary>
        /// <param name="c"></param>
        public AddIngredientPage(RecieptsDBEntities c)
        {
            InitializeComponent();
            context = c;
            //выводим в раскрывающийся список все единицы измерения
            UnitBox.ItemsSource = context.Unit.ToList();
        }
        /// <summary>
        /// конструктор для редактирования.
        /// ing - ингредиент для редактирования
        /// </summary>
        /// <param name="c"></param>
        /// <param name="ing"></param>
        public AddIngredientPage(RecieptsDBEntities c, Ingredient ing)
        {
            InitializeComponent();
            context = c;
            ingredient = ing;
            //меняем надпись на кнопке
            buttonAU.Content = "Редактировать";
            //привязываем к кнопке другой обработчик нажатия
            buttonAU.Click += UpdateClick;
            //заполняем поля на форме
            NameBox.Text = ingredient.Name;
            CountBox.Text = ingredient.CostForCount.ToString();
            CostBox.Text = ingredient.Cost.ToString();
            AvailBox.Text = ingredient.AvailableCount.ToString();
            UnitBox.ItemsSource = context.Unit.ToList();
            UnitBox.SelectedItem = ingredient.Unit;
        }
        /// <summary>
        /// Нажатие на кнопку "Редактировать"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //получаем выбранный элемент раскрывающеося списка
                Unit unit = UnitBox.SelectedItem as Unit;
                //так как наш ingregient находятся в конктексе, можно его отредактировать
                //и  созранить изменения. они отразятся в БД
                ingredient.Name = NameBox.Text;
                ingredient.AvailableCount = Convert.ToDouble(AvailBox.Text);
                ingredient.Cost = Convert.ToDecimal(CostBox.Text);
                ingredient.CostForCount = Convert.ToDouble(CountBox.Text);
                ingredient.UnitId = unit.Id;
                ingredient.Unit = unit;
                //сохраняем изенения БД
                context.SaveChanges();
                //переход к странице Ингрединты
                NavigationService.Navigate(new IngredientPage(context));
            }
            catch
            {
                MessageBox.Show("Ошибка!");
            }

        }
        /// <summary>
        /// Нажатие на кнопку "Отмена"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanselCkick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack(); //возврат на страницу Ингредиенты
        }
        /// <summary>
        /// Нажатие на кнопку "Добавить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddIngredient(object sender, RoutedEventArgs e)
        {
            try
            {
                //получаем выбранную единицу измерения
                Unit unit = UnitBox.SelectedItem as Unit;
                //создаем Ингредиет и заполняем его поля
                Ingredient ing = new Ingredient()
                {
                    Id = context.Ingredient.ToList().Last().Id + 1,
                    Name = NameBox.Text,
                    AvailableCount = Convert.ToDouble(AvailBox.Text),
                    Cost = Convert.ToDecimal(CostBox.Text),
                    CostForCount = Convert.ToDouble(CountBox.Text),
                    UnitId = unit.Id,
                    Unit = unit
                };
                //добавляем ингредиент в БД
                context.Ingredient.Add(ing);
                //созранение изменений
                context.SaveChanges();
                //переход на страницу Ингредиенты
                NavigationService.Navigate(new IngredientPage(context));
            }
            catch(FormatException) //перейдет сюда, если исключение возникло на Convert.To...
            {
                MessageBox.Show("Ошибка вводимых данных!");
            }
            catch //перейдет сюда во всех остальных случаях
            {
                MessageBox.Show("Ошибка!");
            }
        }
    }
}
