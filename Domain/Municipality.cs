using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class Municipality
    {
        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MunicipalityID { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string MunicipalityName { get; set; }
        [NotMapped]
        public virtual ICollection<Vendor> Vendors { get; set; }
        [NotMapped]
        public virtual ICollection<Clinic> Clinics { get; set; }
    }
}
