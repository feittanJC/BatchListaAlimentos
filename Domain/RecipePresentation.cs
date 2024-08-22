using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class RecipePresentation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecipePresentationID { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string RecipePresentationName { get; set; }
        public bool Enabled { get; set; }
        [NotMapped]
        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
