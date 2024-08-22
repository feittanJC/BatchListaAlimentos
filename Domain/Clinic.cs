
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class Clinic
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClinicID { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string ClinicName { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string ClinicNameGM { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string ClinicShortName { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string Address1 { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string Address2 { get; set; }
        public string State { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Phone { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string AddressZipCode { get; set; }
        [Column(TypeName = "decimal(15,10)")]
        public decimal LocationX { get; set; }
        [Column(TypeName = "decimal(15,10)")]
        public decimal LocationY { get; set; }
        //public Point Location { get; set; }
        public bool Enabled { get; set; }
        public int? IDSalud { get; set; }
        public int? IDParticipante { get; set; }
        public int? RegionID { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string OpenClosed { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Supervisor { get; set; }
        public int? MunicipalityID { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string PhoneExtension { get; set; }
        public virtual Municipality Municipality { get; set; }
        public virtual Region Region { get; set; }
        public virtual ICollection<Appointment > Appointments { get; set; }
    }
}
