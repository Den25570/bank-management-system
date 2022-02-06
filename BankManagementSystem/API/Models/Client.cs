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

        public Database.Client ToDatabaseModel(Database.Client databaseModel = null)
        {
            if (databaseModel == null)
                databaseModel = new Database.Client();

            databaseModel.Firstname = Firstname;
            databaseModel.Lastname = Lastname;
            databaseModel.Middlename = Middlename;
            databaseModel.Birthday = Birthday;
            databaseModel.Sex = Sex;
            databaseModel.PassportIdNumber = PassportIdNumber;
            databaseModel.PassportNumber = PassportNumber;
            databaseModel.PassportSeries = PassportSeries;
            databaseModel.PassportIssuer = PassportIssuer;
            databaseModel.PassportIssueDate = PassportIssueDate;
            databaseModel.BirthPlace = BirthPlace;
            databaseModel.ResidenceCityId = ResidenceCityId;
            databaseModel.ResidenceAddress = ResidenceAddress;
            databaseModel.PhoneNumberStationary = PhoneNumberStationary;
            databaseModel.PhoneNumberMobile = PhoneNumberMobile;
            databaseModel.Email = Email;
            databaseModel.FamilyStatusId = FamilyStatusId;
            databaseModel.CitizenshipId = CitizenshipId;
            databaseModel.RegistrationCityId = RegistrationCityId;
            databaseModel.RegistrationAddress = RegistrationAddress;
            databaseModel.DisabilityId = DisabilityId;
            databaseModel.IsRetired = IsRetired;
            databaseModel.MounthlyIncome = MounthlyIncome;

            if (MounthlyIncome != null && MounthlyIncome < 0)
            {
                throw new HttpResponseException(400, $"Доход не может быть отрицательным");
            }
            if (PassportIssueDate > DateTime.Now || PassportIssueDate < Birthday)
            {
                throw new HttpResponseException(400, $"Некорректная дата выдачи паспорта");
            }
            if (Birthday > DateTime.Now || PassportIssueDate < Birthday)
            {
                throw new HttpResponseException(400, $"Некорректная дата рождения");
            }

            return databaseModel;
        }
    }
}
