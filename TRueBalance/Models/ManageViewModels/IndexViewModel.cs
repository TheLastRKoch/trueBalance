using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TRueBalance.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }


        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Numero de telefono")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [Display(Name = "URL imagen")]
        public string ImgUrl { get; set; }
        
        [Display(Name = "Fecha de nacimiento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public string BirthDay { get; set; }

        public string StatusMessage { get; set; }
    }
}
