using System.ComponentModel.DataAnnotations;

namespace LadyLuxe.Models.Domain
{
    public class User
    {
        [Key]
        [MaxLength(50)]// nimezoea hii..but ni sawa
        public string id { get; set; }
        [Required]
        public string Fullname { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [Required]
        public string role { get; set; }
             
    }//naona hii model iko sawa....
}
