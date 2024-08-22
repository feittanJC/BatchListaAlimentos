using Microsoft.EntityFrameworkCore;
using Common.Settings;
using Domain;


namespace Infraestructura
{
    public class ClientDbContext : DbContext
    {
        private IConfigurationLib config;

        public ClientDbContext() : base() { }

        public ClientDbContext(DbContextOptions<ClientDbContext> options) : base(options) { }


        public DbSet<FoodClassification> FoodClassifications { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<VendorFood> VendorFoods { get; set; }
        public DbSet<Factory> Factories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<FoodType> FoodTypes { get; set; }
        public DbSet<FoodSubClassification> FoodSubClassifications { get; set; }
        public DbSet<FoodSubClassificationCode> FoodSubClassificationCodes { get; set; }
        public DbSet<ClassificationRule> ClassificationRules { get; set; }
        public DbSet<FoodVariety> FoodVarieties { get; set; }
        public DbSet<FoodPackage> FoodPackages { get; set; }
        public DbSet<TypeProduct> TypeProducts { get; set; }
        public DbSet<RecipePresentation> RecipePresentations { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Preparation> Preparations { get; set; }
        public DbSet<Punctuation> Punctuations { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }


    }
}
