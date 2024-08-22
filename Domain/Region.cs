using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class Region
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegionID { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string RegionName { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string RegionNameGM { get; set; }
        public bool Enabled { get; set; }
        [NotMapped]
        public virtual ICollection<Vendor> Vendors { get; set; }
        [NotMapped]
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
