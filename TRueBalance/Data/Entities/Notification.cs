using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TRueBalance.Data.Entities
{
    public enum NotificationType
    {
        Banner, Message, Error
    }

    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationID { get; set; }
        public NotificationType Type  { get; set; }
        public string Text { get; set; }
    }
}
