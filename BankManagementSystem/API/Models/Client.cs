using API.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"([а-яА-Я]|[a-zA-Z])+")]
        public string Firstname { get; set; }
        [Required]
        [RegularExpression(@"([а-яА-Я]|[a-zA-Z]|-)+")]
        public string Lastname { get; set; }
        [Required]
        [RegularExpression(@"([а-яА-Я]|[a-zA-Z])+")]
        public string Middlename { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public bool Sex { get; set; }
        [Required]
        [RegularExpression(@"[0-9]{7}[а-яА-Я][0-9]{3}[а-яА-Я]{2}[0-9]")]
        public string PassportIdNumber { get; set; }
        [Required]
        [RegularExpression(@"[0-9]{7}")]
        public string PassportNumber { get; set; }
        [Required]
        [RegularExpression(@"[а-яА-Я]{2}")]
        public string PassportSeries { get; set; }
        [Required]
        public string PassportIssuer { get; set; }
        [Required]
        public DateTime PassportIssueDate { get; set; }
        [Required]
        public string BirthPlace { get; set; }
        [Required]
        public int ResidenceCityId { get; set; }

        public string? ResidenceAddress { get; set; }
        [RegularExpression(@"[+]?[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\.\/0-9]{8,9}")]
        public string? PhoneNumberStationary { get; set; }
        [RegularExpression(@"[+]?[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\.\/0-9]{5,9}")]
        public string? PhoneNumberMobile { get; set; }
        [RegularExpression(@"[a-zA-Z0-9.!#$%&’*+\/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*")]
        public string? Email { get; set; }

        [Required]
        public int FamilyStatusId { get; set; }
        [Required]
        public int CitizenshipId { get; set; }
        [Required]
        public int RegistrationCityId { get; set; }
        [Required]
        public string RegistrationAddress { get; set; }
        [Required]
        public int DisabilityId { get; set; }
        [Required]
        public bool IsRetired { get; set; }
        public decimal? MounthlyIncome { get; set; }
    }
}
