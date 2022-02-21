using API.Database;
using API.Exceptions;

namespace API.Extensions
{
    public static class DatabaseExtensions
    {
        public static Database.Client ToDatabaseModel(this Models.Client client, Database.Client databaseModel = null)
        {
            if (databaseModel == null)
                databaseModel = new Database.Client();

            databaseModel.Firstname = client.Firstname;
            databaseModel.Lastname = client.Lastname;
            databaseModel.Middlename = client.Middlename;
            databaseModel.Birthday = client.Birthday;
            databaseModel.Sex = client.Sex;
            databaseModel.PassportIdNumber = client.PassportIdNumber;
            databaseModel.PassportNumber = client.PassportNumber;
            databaseModel.PassportSeries = client.PassportSeries;
            databaseModel.PassportIssuer = client.PassportIssuer;
            databaseModel.PassportIssueDate = client.PassportIssueDate;
            databaseModel.BirthPlace = client.BirthPlace;
            databaseModel.ResidenceCityId = client.ResidenceCityId;
            databaseModel.ResidenceAddress = client.ResidenceAddress;
            databaseModel.PhoneNumberStationary = client.PhoneNumberStationary;
            databaseModel.PhoneNumberMobile = client.PhoneNumberMobile;
            databaseModel.Email = client.Email;
            databaseModel.FamilyStatusId = client.FamilyStatusId;
            databaseModel.CitizenshipId = client.CitizenshipId;
            databaseModel.RegistrationCityId = client.RegistrationCityId;
            databaseModel.RegistrationAddress = client.RegistrationAddress;
            databaseModel.DisabilityId = client.DisabilityId;
            databaseModel.IsRetired = client.IsRetired;
            databaseModel.MounthlyIncome = client.MounthlyIncome;

            if (client.MounthlyIncome != null && client.MounthlyIncome < 0)
            {
                throw new HttpResponseException(400, $"Доход не может быть отрицательным");
            }
            if (client.PassportIssueDate > DateTime.Now || client.PassportIssueDate < client.Birthday)
            {
                throw new HttpResponseException(400, $"Некорректная дата выдачи паспорта");
            }
            if (client.Birthday > DateTime.Now || client.PassportIssueDate < client.Birthday)
            {
                throw new HttpResponseException(400, $"Некорректная дата рождения");
            }

            return databaseModel;
        }

        public static decimal GetPercents(this Credit credit, DateTime date)
        {
            var deltaDays = (date - credit.LastPercentEvaluationDate).Days;
            var percentValue = (deltaDays / 365M) * (credit.CreditPercent / 100M);
            if (credit.CreditTypeId == 1) percentValue *= credit.CreditAmount;
            else if (credit.CreditTypeId == 2) percentValue *= (credit.CreditAmount - credit.PayedToDate);
            return percentValue;
        }

        public static decimal GetMainPaymentValue(this Credit credit, DateTime date)
        {
            var deltaDays = (date - credit.LastPercentEvaluationDate).Days * 1M;
            var value = deltaDays  / (credit.EndDate - credit.StartDate).Days;
            if (credit.CreditTypeId == 1) value *= 0;
            else if (credit.CreditTypeId == 2) value *= credit.CreditAmount;
            return value;
        }

        public static decimal GetPercents(this Deposit deposit, DateTime date)
        {
            var deltaDays = (date - deposit.LastPercentEvaluationDate).Days;
            var percentValue = (deltaDays / 365M) * (deposit.DepositPercent / 100M) * deposit.DepositAmount;
            return percentValue;
        }

        
    }
}
