using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!IsValidUserInput(firstName, lastName, email, dateOfBirth))
            {
                return false;
            }

            var client = GetClientById(clientId);
            if (client == null)
            {
                return false;
            }

            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = email,
                DateOfBirth = dateOfBirth,
                Client = client
            };

            if (!IsUserEligibleForCredit(user, client))
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        private bool IsValidUserInput(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            return !string.IsNullOrWhiteSpace(firstName) &&
                   !string.IsNullOrWhiteSpace(lastName) &&
                   IsValidEmail(email) &&
                   IsOfLegalAge(dateOfBirth);
        }

        private bool IsValidEmail(string email)
        {
            // Simple e-mail validation
            return email.Contains("@") && email.Contains(".");
        }

        private bool IsOfLegalAge(DateTime dateOfBirth)
        {
            // Assuming legal age is 21
            var age = DateTime.Now.Year - dateOfBirth.Year;
            if (dateOfBirth > DateTime.Now.AddYears(-age)) age--;
            return age >= 21;
        }

        private Client GetClientById(int clientId)
        {
            // Simulating database access
            var clientRepository = new ClientRepository();
            return clientRepository.GetById(clientId);
        }

        private bool IsUserEligibleForCredit(User user, Client client)
        {
            // Assume that this method will set the HasCreditLimit and CreditLimit properties of the user
            // according to the business logic based on the client type and other factors
            // ...

            // If a user has a credit limit, it must be above a certain threshold to be eligible
            return !user.HasCreditLimit || user.CreditLimit >= 500;
        }
    }
}

