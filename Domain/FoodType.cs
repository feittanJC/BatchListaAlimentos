using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class FoodType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FoodTypeID { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string FoodTypeName { get; set; }
        public bool Enabled { get; set; }
        [NotMapped]
        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
