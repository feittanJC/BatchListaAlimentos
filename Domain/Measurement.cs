using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class Measurement
    {
        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MeasurementID { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string MeasurementName { get; set; }
        public bool Enabled { get; set; }
        [NotMapped]
        public virtual ICollection<Food> Foods { get; set; }
    }
}
