using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Database
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Middlename { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public bool Sex { get; set; }
        [Required]
        [MaxLength(14)]
        public string PassportIdNumber { get; set; }
        [Required]
        [MaxLength(9)]
        public string PassportNumber { get; set; }
        [Required]
        [MaxLength(2)]
        public string PassportSeries { get; set; }
        [Required]
        public string PassportIssuer { get; set; }
        [Required]
        public DateTime PassportIssueDate { get; set; }
        [Required]
        public string BirthPlace { get; set; }
        [Required]
        [ForeignKey("ResidenceCity")]
        public int ResidenceCityId { get; set; }
        public string? ResidenceAddress { get; set; }
        [MaxLength(15)]
        public string? PhoneNumberStationary { get; set; }
        [MaxLength(15)]
        public string? PhoneNumberMobile { get; set; }
        public string? Email { get; set; }
        [Required]
        [ForeignKey("FamilyStatus")]
        public int FamilyStatusId { get; set; }
        [Required]
        public int CitizenshipId { get; set; }
        [Required]
        [ForeignKey("RegistrationCity")]
        public int RegistrationCityId { get; set; }
        [Required]
        public string RegistrationAddress { get; set; }
        [Required]
        [ForeignKey("Disability")]
        public int DisabilityId { get; set; }
        [Required]
        public bool IsRetired { get; set; }
        public decimal? MounthlyIncome { get; set; }

        public City ResidenceCity { get; set; }
        public City RegistrationCity { get; set; }
        public FamilyStatus FamilyStatus { get; set; }
        public Disability Disability { get; set; }
        public Citizenship Citizenship { get; set; }
    }
}
