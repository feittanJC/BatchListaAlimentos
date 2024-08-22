using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class Vendor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VendorID { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string VendorName { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string VendorAddress1 { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string VendorAddress2 { get; set; }
        public string State { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string AddressZipCode { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string VendorPhone { get; set; }
        [Column(TypeName = "decimal(15,10)")]
        public decimal LocationX { get; set; }
        [Column(TypeName = "decimal(15,10)")]
        public decimal LocationY { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public bool Enabled { get; set; }
        [Column(TypeName ="varchar(50)")]
        public string Sello { get; set; }   
        public int? RegionID { get; set; }
        public int? MunicipalityID { get; set; }
        public virtual Municipality Municipality { get; set; }
        public virtual Region Region { get; set; }
        public virtual ICollection<VendorFood> VendorFoods { get; set; }
    }
}
