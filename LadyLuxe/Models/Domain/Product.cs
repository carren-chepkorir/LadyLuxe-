using System.ComponentModel.DataAnnotations;

namespace LadyLuxe.Models.Domain
{
    public class Product
    {
        //iko sawa....nope kuna issue..umeona??
        public Guid Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string CategoryId { get; set; }
        [Required]
        public string Sub_CategoryId { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]//hii hapa
        public string Description { get; set; }
        [Required]
        public double PreviousPrice { get; set; }
        [Required]
        public int Quality { get; set; }
        public string Image { get; set; }
    
       
    }
}
