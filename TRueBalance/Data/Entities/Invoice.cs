using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRueBalance.Data.Entities
{
    public class Invoice
    {
        public Invoice()
        {
            LinkedSells = new HashSet<Sell>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceID { get; set; }

        public string ClientName { get; set; }

        public string Vendor { get; set; }

        public string State { get; set; }

        public int TotalToPay { get; set; }

        public int Cash { get; set; }

        public Category PaymentType { get; set; }

        [DataType(DataType.Date)]
        [StringLength(10)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:MM}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public virtual ICollection<Sell> LinkedSells { get; set; }

    }
}
