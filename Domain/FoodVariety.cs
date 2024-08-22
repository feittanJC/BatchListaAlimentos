using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class FoodVariety
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FoodVarietyID { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string FoodVarietyNameEN { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string FoodVarietyNameES { get; set; }
        public int VarietyOrder { get; set; }
        public bool Enabled { get; set; }
    }
}
