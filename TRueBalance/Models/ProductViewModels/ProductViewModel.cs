using TRueBalance.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRueBalance.Models.ProductViewModels
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }

        [Required (ErrorMessage = "Este campo no puede quedar vacio")]
        [Display(Name = "Codigo")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Este campo no puede quedar vacio")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El precio ingresado no es valido")]
        [Display(Name = "Precio")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Este campo no puede quedar vacio")]
        [Display(Name = "Fecha de vencimiento")]
        public DateTime DueDate { get; set; }
    }
}
