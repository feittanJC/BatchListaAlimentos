using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Food
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FoodID { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string FoodName { get; set; }
        public string ImagePath { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string UPCScan { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string TypeProduct { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal? Quantity { get; set; }
        public bool Enabled { get; set; }
        public int? FactoryID { get; set; }
        //[ForeignKey("FactoryID")]
        public int? FoodClassificationID { get; set; }
        public int? FoodSubClassificationID { get; set; }
        public int? FoodSubClassificationCodeID { get; set; }
        public int? ClassificationRuleID { get; set; }

        public string FoodClassificationName { get; set; }
        public string FoodSubClassificationName { get; set; }
        public int? TypeProductID { get; set; }
        public int? FoodPackageID { get; set; }
        public int? FoodVarietyID { get; set; }
        [Column(TypeName = "varchar(300)")]
        public string PackageNameES { get; set; }
        [Column(TypeName = "varchar(300)")]
        public string PackageNameEN { get; set; }
        [Column(TypeName = "varchar(300)")]
        public string VarietyNameES { get; set; }
        [Column(TypeName = "varchar(300)")]
        public string VarietyNameEN { get; set; }
        public int? VarietyOrder { get; set; }

        //[ForeignKey("FactoryID")]
        public int? MeasurementID { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdateOn { get; set; }
        //[ForeignKey("FactoryID")]
        public virtual Factory Factory { get; set; }
        public virtual FoodClassification FoodClassification { get; set; }
        public virtual FoodSubClassification FoodSubClassification { get; set; }
        public virtual FoodSubClassificationCode FoodSubClassificationCode { get; set; }
        public virtual ClassificationRule ClassificationRule { get; set; }
        public virtual TypeProduct TypeProducts { get; set; }
        public virtual FoodPackage FoodPackage { get; set; }
        public virtual FoodVariety FoodVariety { get; set; }

        public virtual Measurement Measurement { get; set; }
        public virtual ICollection<VendorFood> VendorFoods { get; set; }
    }
}
