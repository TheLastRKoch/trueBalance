using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRueBalance.Data.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public string Code { get; set; }

        [DataType(DataType.Date)]
        [StringLength(10)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",  ApplyFormatInEditMode = true )]
        public DateTime DueDate { get; set; }
    }
}
