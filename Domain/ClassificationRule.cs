using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class ClassificationRule
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClassificationRuleID { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string ClassificationRuleName { get; set; }
        public bool Enabled { get; set; }
    }
}
