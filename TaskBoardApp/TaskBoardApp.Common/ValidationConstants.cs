namespace TaskBoardApp.Common;

public static class ValidationConstants
{
    public static class TaskEntityValidations
    {
        public const int TaskTitleMaxLength = 70;

        public const int TaskTitleMinLength = 5;

        public const int TaskDescriptionMaxLength = 1000;

        public const int TaskDescriptionMinLength = 10;
    }

    public static class BoardEntityValidations
    {
        public const int BoardNameMaxLength = 30;

        public const int BoardNameMinLength = 3;
    }
}
