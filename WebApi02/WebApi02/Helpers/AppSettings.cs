namespace WebApi02.Helpers
{
    public class AppSettings
    {
        public AppSettings()
        {
            this.Secret = "THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING";
        }
        public string Secret { get; set; }
    }
}