using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class UserQualification
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserQualificationID { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal? Qualification { get; set; }
        public int? RecipeID { get; set; }
        public virtual Recipe Recipe { get; set; }
        public int? UserID { get; set; }
        public virtual User User { get; set; }
    }
}
