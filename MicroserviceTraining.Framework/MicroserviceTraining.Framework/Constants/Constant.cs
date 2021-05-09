namespace MicroserviceTraining.Framework.Constants
{
    public static class Constant
    {
        public const string ResultCode_Success = "0000";

        public const string IntegrationEventPrefix = "INTEGRATION_EVENT_";

        public const string EventTopic_TournamentAdded = "NewTournamentAdded";
        public const string EventTopic_TournamentDateChanged = "TournamentDateChanged";
        public const string EventTopic_TournamentPriceChanged = "TournamentPriceChanged";
        public const string EventTopic_CheckoutAccepted = "CheckoutAccepted";
        public const string EventTopic_PaymentCompleted = "PaymentCompleted";

        public const int MinRatingForTournamentInforming = 2000;
    }
}
