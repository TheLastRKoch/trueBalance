using TRueBalance.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TRueBalance.Data.Entities
{
    public class PrintQueue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PrintElementID { get; set; }

        public virtual Invoice LinkedInvoice { get; set; }

    }
}
