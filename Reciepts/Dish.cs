//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Reciepts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class Dish
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Dish()
        {
            this.CookingStage = new HashSet<CookingStage>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int ServingQuantity { get; set; }
        public int CategoryId { get; set; }
        public string RecipeLink { get; set; }
        public byte[] Photo { get; set; }

        public string Price
        {
            get
            {
                decimal sum = 0;
                foreach (var item in CookingStage)
                {
                    sum += item.IngredientOfStage.Sum(x => x.Ingredient.Cost);
                }
                return $"1 порция = {sum / ServingQuantity}";
            }
        }

        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CookingStage> CookingStage { get; set; }
    }
}
