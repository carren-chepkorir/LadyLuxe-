namespace LadyLuxe.Models.Domain
{
    public class Cart
    {
        // so tuunde..action method ya kuweka kwa list..but statick one
        public string Id { get; set; }///
        public string ProductId { get; set; } //hatukuchukua product name
        public int Quantity { get; set; }
        public string ImageURL { get; set; }// ooh kumbe iko
        public Double price { get; set; }
        public Double subtotal { get; set; }
        public string ProductName { get; set; }
    }
}

//.. kiasi tu a minsaawa..najua tu mtu ameenda washrooms..hahah zii ni vile nimeenda kuulizia stuff fulani
//hehe sasawa