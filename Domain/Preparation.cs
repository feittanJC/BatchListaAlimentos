using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class Preparation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PreparationID { get; set; }
        public int Ordinal { get; set; }

        [Column(TypeName = "varchar(500)")]
        public string PreparationDescription { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string PreparationGroup { get; set; }
        public bool Enabled { get; set; }
        public int? RecipeID { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
