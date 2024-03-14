namespace MvcProxy.Models
{
    public class DiscoverModel
    {
        public string? Name { get; set; } = "Shop Labwork";
        public string? Url { get; set; } = "shop.sudohub.dev";
        public string? Img { get; set; } = "https://pickystory.com/wp-content/uploads/2022/08/advantages-of-ecommerce-shopping-cart.png";
        public bool Auth { get; set; } = true;
        public bool Online { get; set; } = true;
        //required specifically for direct forwarding
        public string? Ip { get; set; } = "192.168.0.102";
        public int Port { get; set; } = 8008;
        public string Path { get; set; } = "";
        public bool Direct { get; set; } = true;
        public bool Secure { get; set; } = false;
        public bool Important { get; set; } = true;

        //misc functions
#pragma warning disable IDE1006 // Naming Styles
        public Uri getReverseURL()
        {
            string url = Secure ? "https://" : "http://";
            url += Ip + ":" + Port + "/" + Path;
            return new Uri(url);
        }
        public Uri getLinkURL()
        {
            return new Uri((Secure ? "https://" : "http://") + Url);
        }
#pragma warning restore IDE1006 // Naming Styles
    }
}
