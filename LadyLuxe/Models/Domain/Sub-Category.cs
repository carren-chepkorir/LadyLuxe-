using System.ComponentModel;

namespace LadyLuxe.Models.Domain
{
    public class Sub_Category
    {
        public Guid Id { get; set; }
        public string GategoryId { get; set; }
        [DisplayName("Sub-Category Name")]
        public string CategoryName{ get; set; }
        public string Icon { get; set; }
        public bool Deletedstatus { get; set; }
    }
}
