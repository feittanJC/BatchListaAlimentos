using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Domain
{
    public class User
    {
        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        //public string UserId { get; set; }
        [Column(TypeName = "varchar(80)")]
        public string UserName { get; set; }
        [Column(TypeName = "varchar(80)")]
        public string FirstName { get; set; }
        [Column(TypeName = "varchar(80)")]
        public string SecondName { get; set; }
        [Column(TypeName = "varchar(80)")]
        public string FirstLastName { get; set; }
        [Column(TypeName = "varchar(80)")]
        public string SecondLastName { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string WicID { get; set; }
        [Column(TypeName = "date")]
        public DateTime Birthdate { get; set; }
        [Column(TypeName = "date")]
        public DateTime CertificationDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime ExpirationDate { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Password { get; set; }
        public bool Enabled { get; set; }
        public bool HighRisk { get; set; }
        public int? RoleID { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Email { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string PhoneMobile { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string AddressLine1 { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string AddressLine2 { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string AddressZipCode { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Office { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string region { get; set; }
        [Column(TypeName = "datetime")]

        [NotMapped]
        public string officeGM { get; set; }


        [NotMapped]
        public string regionGM { get; set; }
       

        public DateTime? CreateDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateWicIDDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ChangePasswordDate { get; set; }
        //public virtual ICollection<UserEducation> UserEducations { get; set; }
        //public virtual ICollection<Message> Messages { get; set; }
        //public virtual ICollection<Document> Documents { get; set; }
        public virtual Role Role { get; set; }
        [NotMapped]
        public string Name { get; set; }
        [NotMapped]
        public string NewPassword { get; set; }
        [NotMapped]
        public virtual ICollection<Notification> Notifications { get; set; }
        [Column(TypeName = "varchar(300)")]
        public string Representative { get; set; }
        [Column(TypeName = "varchar(500)")]
        public string Endorser1 { get; set; }
        [Column(TypeName = "varchar(500)")]
        public string Endorser2 { get; set; }
        [Column(TypeName = "varchar(300)")]
        public string Telephone { get; set; }
        public bool ViewCard { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string OriginalPassword { get; set; }
        public bool Permision1 { get; set; }
        public bool Permision2 { get; set; }
        public bool Permision3 { get; set; }
        public bool Permision4 { get; set; }
        public bool? Permision5 { get; set; }
        public bool? Permision6 { get; set; }
        public bool? Permision7 { get; set; }
        public bool? Permision8 { get; set; }
        public bool? Permision9 { get; set; }
        public bool? Permision10 { get; set; }
        public bool TermsAndConditions { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TermsAndConditionsDate { get; set; }

        [NotMapped]
        public int Page { get; set; }
        [NotMapped]
        public int RowsList { get; set; }

        [Column(TypeName = "varchar(80)")]
        public string SlugFirstName { get; set; }
        [Column(TypeName = "varchar(80)")]
        public string SlugSecondName { get; set; }
        [Column(TypeName = "varchar(80)")]
        public string SlugFirstLastName { get; set; }
        [Column(TypeName = "varchar(80)")]
        public string SlugSecondLastName { get; set; }
        [NotMapped]
        public int? Status { get; set; }
        [NotMapped]
        public int? Readered { get; set; }

        public int NroMessagesReadered
        {
            get; set;
        }
        public int NroMessagesNoReadered
        {
            get; set;
        }
        public int NroDocumentsAcept
        {
            get; set;
        }
        public int NroDocumentsRefuse
        {
            get; set;
        }
        public int NroDocumentsNoCheck
        {
            get; set;
        }

        [NotMapped]
        public int TotalStatus { get; set; }
        [NotMapped]
        public int TotalRead { get; set; }
        [NotMapped]
        public string? ParentGuardianName
        { get; set; }

        [Column(TypeName ="varchar(50)")]
        public  string? FamilyID { get; set; }

        public bool? ParentGuardianIn { get; set; }

        public string? Category { get; set; }
    }
}
