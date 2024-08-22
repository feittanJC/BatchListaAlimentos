using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class FoodSubClassification
    {
        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FoodSubClassificationID { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string FoodSubClassificationName { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string ImagePath { get; set; }
        public bool Enabled { get; set; }

        public int Orden { get; set; }

        public int? FoodClassificationID { get; set; }
        public virtual FoodClassification FoodClassification { get; set; }
        [NotMapped]
        public int TotalFoods { get; set; }

    }
}
