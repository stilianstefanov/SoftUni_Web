namespace Contacts.Common
{
    public static class ValidationConstants
    {
        public static class ContactEntityValidations
        {
            public const int FirstNameMinLength = 2;

            public const int FirstNameMaxLength = 50;

            public const int LastNameMinLength = 5;

            public const int LastNameMaxLength = 50;

            public const int EmailMinLength = 10;

            public const int EmailMaxLength = 60;

            public const int PhoneNumberMinLength = 10;

            public const int PhoneNumberMaxLength = 13;

            public const string PhoneNumberRegex = @"^(?:\+359|0)(?:\s?\d{3}(?:[\s-]?\d{2}){3})$";

            public const string WebsiteRegex = @"^www\.[a-zA-Z0-9\-]+\.bg$";
        }

    }
}