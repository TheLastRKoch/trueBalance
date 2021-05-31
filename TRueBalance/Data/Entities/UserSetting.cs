using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRueBalance.Data.Entities
{
    public class UserSetting
    {
        [Key]
        public int SettingID { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string User { get; set; }
        public virtual Category Category { get; set; }
    }
}
