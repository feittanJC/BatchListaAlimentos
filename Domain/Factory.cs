using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    //[Table("Factories", Schema = "dbo")]
    public class Factory
    {
        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FactoryID { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string FactoryName { get; set; }

        public bool Enabled { get; set; }
        [NotMapped]
        public virtual ICollection<Food> Foods { get; set; }
    }
}
