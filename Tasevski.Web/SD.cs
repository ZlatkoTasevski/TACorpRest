namespace Tasevski.Web
{
    public static class SD
    {
        public static string ProductAPIBase { get; set; }
        public static string ShoppingCartAPIBase { get; set; }
        public static string CouponAPIBase { get; set; }
        public static string UserAPIBase { get; set; }

        public const string Admin = "Admin";
        public const string Customer = "Customer";
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}