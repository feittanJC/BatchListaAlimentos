using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class Notification
    { 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationID { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string NotificationTitle { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string NotificationDescription { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool Enabled { get; set; }
    }
}
