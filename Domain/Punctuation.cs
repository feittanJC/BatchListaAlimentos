using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class Punctuation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PunctuationID { get; set; }
        public int Value { get; set; }
        public int RecipeID { get; set; }
        public virtual Recipe Recipe { get; set; }
        public bool Enabled { get; set; }
    }
}
