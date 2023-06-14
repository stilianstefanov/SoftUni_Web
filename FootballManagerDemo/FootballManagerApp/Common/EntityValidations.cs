namespace FootballManagerApp.Common;

public static class EntityValidations
{
    public static class PlayerValidations
    { 
        public const int FullNameMaxLength = 80;
        public const int FullNameMinLength = 5;

        public const int PositionMaxLength = 20;
        public const int PositionMinLength = 5;

        public const int DescriptionMaxLength = 200;

        public const int SpeedMinValue = 0;
        public const int SpeedMaxValue = 10;

        public const int EnduranceMinValue = 0;
        public const int EnduranceMaxValue = 10;
    }
}
