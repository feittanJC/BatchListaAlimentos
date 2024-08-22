using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class Ingredient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IngredientID { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string IngredientDescription { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string IngredientGroup { get; set; }
        public bool Enabled { get; set; }
        public int? RecipeID { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
