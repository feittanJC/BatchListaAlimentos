using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class Appointment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentID { get; set; }
        public int? ClinicID { get; set; }
        public int? UserID { get; set; }
        [Column(TypeName = "varchar(120)")]
        public string Title { get; set; }
        [Column(TypeName = "varchar(300)")]
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public virtual Clinic Clinic { get; set; }
        public virtual User User { get; set; }
    }
}
