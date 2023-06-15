namespace BusStationApp.Common;

public static class ValidationConstants
{
    public static class DestinationValidations
    {
        public const int DestinationNameMaxLength = 50;

        public const int DestinationNameMinLength = 2;

        public const int OriginMaxLength = 50;

        public const int OriginMinLength = 2;

        public const int DateMaxLength = 30;

        public const int TimeMaxLength = 30;
    }

    public static class TicketValidations
    {
        public const string PriceMinValue = "10";

        public const string PriceMaxValue = "90";

        public const int TicketsCountMinValue = 1;

        public const int TicketsCountMaxValue = 10;
    }
}
