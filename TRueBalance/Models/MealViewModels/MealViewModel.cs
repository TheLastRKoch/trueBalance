using System.ComponentModel.DataAnnotations;

namespace TRueBalance.Models.MealViewModels
{
    public class MealViewModel
    {
        public string CSVData { get; set; }

        public int MealID { get; set; }

        [Required(ErrorMessage = "Este campo no puede quedar vacio")]
        [Display(Name = "Digite el nombre del platillo")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Este campo no puede quedar vacio")]
        [Range(1,100000)]
        [DataType(DataType.Currency, ErrorMessage = "El valor no puede ser 0")]
        [Display(Name = "Digite el precio del platillo")]
        public int Price { get; set; }

        public string URL { get; set; }

    }

    public class MealModel
    {
        public string Name { get; set; }

        public int Price { get; set; }

        public string Category { get; set; }

        public MealModel()
        {

        }

        public MealModel(string _name, int _price, string _category)
        {
            this.Name = _name;
            this.Price = _price;
            this.Category = _category;
        }
    }
}
