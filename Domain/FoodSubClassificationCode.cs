using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class FoodSubClassificationCode
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FoodSubClassificationCodeID { get; set; }
        [Column(TypeName = "varchar(7)")]
        public string FoodSubClassificationCodeValue { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string FoodSubClassificationCodeDescription { get; set; }
        public bool Enabled { get; set; }
        public int FoodSubClassificationID { get; set; }
        public virtual FoodSubClassification FoodSubClassification { get; set; }
    }
}
