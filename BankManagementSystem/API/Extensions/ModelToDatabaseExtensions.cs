using API.Exceptions;

namespace API.Extensions
{
    public static class ModelToDatabaseExtensions
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
    }
}
