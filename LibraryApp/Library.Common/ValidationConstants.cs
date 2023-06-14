namespace Library.Common;

public static class ValidationConstants
{
    public static class BookEntityValidations
    {
        public const int BookTitleMaxLength = 50;

        public const int BookTitleMinLength = 10;

        public const int BookAuthorMaxLength = 50;

        public const int BookAuthorMinLength = 5;

        public const int BookDescriptionMaxLength = 5000;

        public const int BookDescriptionMinLength = 5;

        public const string BookRatingMaxValue = "10";

        public const string BookRatingMinValue = "0";
    }

    public static class CategoryValidations
    {
        public const int CategoryNameMaxLength = 50;

        public const int CategoryNameMinLength = 5;
    }
}
