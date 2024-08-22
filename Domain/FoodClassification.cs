using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class FoodClassification
    {
        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FoodClassificationID { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string FoodClassificationName { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string FoodClassificationCode { get; set; }
        public bool Enabled { get; set; }
        public virtual ICollection<FoodSubClassification> FoodSubClassifications { get; set; }
        public string ImagePath { get; set; }
        public int Orden { get; set; }

        [NotMapped]
        public virtual ICollection<Food> Foods { get; set; }
    }
}
