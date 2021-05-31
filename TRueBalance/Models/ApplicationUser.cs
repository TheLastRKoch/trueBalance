using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TRueBalance.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        : base()
        {
        }

        public ApplicationUser(string userName, string firstName, string lastName, DateTime birthDay, string ImgURl)
            : base(userName)
        {
            base.Email = userName;

            this.FirstName = firstName;
            this.LastName = lastName;
            this.BirthDay = birthDay;
            this.ImgURl = ImgURl;

        }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }

        public string FullName => $"{this.FirstName} {this.LastName}";

        [StringLength(500)]
        public string ImgURl { get; set; }
    }
}
