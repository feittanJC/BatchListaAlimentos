using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class TypeProduct
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TypeProductID { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string TypeProductName { get; set; }
        public bool Enabled { get; set; }
    }
}
