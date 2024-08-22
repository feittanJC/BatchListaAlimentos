using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class FoodPackage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FoodPackageID { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string FoodPackageNameEN { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string FoodPackageNameES { get; set; }
        public bool Enabled { get; set; }
    }
}
