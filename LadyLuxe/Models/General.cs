using LadyLuxe.Models.Domain;

namespace LadyLuxe.Models
{
    public class General
    {
      public  List<User> users { get; set; }
        public List<Category> categories { get; set; }
        public List<Sub_Category> sub_Categories { get; set; }
        public List<Product> products { get; set; }
        public List<Cart> carts { get; set; }//pole sikua nim

        //so this class tutatumia all through..
    }
}
