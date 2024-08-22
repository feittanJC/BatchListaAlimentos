using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class Recipe
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecipeID { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string RecipeName { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string RecipeDescription { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string ImagePath { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string RecipePath { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string Portion { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string TimePreparation { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string TimeCoking { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string TimeFreezing { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string TimeFullPreparation { get; set; }
        public string Note { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal? Qualification { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Calorie { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Carbohydrate { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Protein { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Grease { get; set; }
        public bool Enabled { get; set; }
        public int? FoodClassificationID { get; set; }
        public virtual FoodClassification FoodClassification { get; set; }
        public int? FoodTypeID { get; set; }
        public virtual FoodType FoodType { get; set; }
        public int? DifficultyID { get; set; }
        public virtual Difficulty Difficulty { get; set; }
        public int? RecipePresentationID { get; set; }
        public virtual RecipePresentation RecipePresentation { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public virtual ICollection<Preparation> Preparations { get; set; }
        public virtual ICollection<UserQualification> UserQualifications { get; set; }
        public virtual ICollection<Punctuation> Points { get; set; }
    }
}
