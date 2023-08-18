using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [DisplayName("Qty")]
        public int Quality { get; set; }
        public string Image { get; set; } //hii itabebat tu image URL...
       
        [NotMapped]
        [DisplayName("Image")]     
        public IFormFile ImageFile { get; set; }

//Hatukueka ya deleted status..but ni sawa tu...
    }
}
