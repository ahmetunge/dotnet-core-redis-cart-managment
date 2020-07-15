namespace Business.Helpers
{
    public static class Messages
    {
        public static string PriceLessThanZeroError { get; set; } = "Price must be greater than 0";

        public static string QuantityLessThanOneError { get; set; } = "Quantity must be at least 1";

        public static string NullKeyError { get; set; } = "Key can not be null or empty";
    }
}