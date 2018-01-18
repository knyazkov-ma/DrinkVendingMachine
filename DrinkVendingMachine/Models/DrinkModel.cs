using DrinkVendingMachine.Resources;
using System.ComponentModel.DataAnnotations;

namespace DrinkVendingMachine.Models
{
    public class DrinkModel
    {
        
        public long Id { get; set; }

        [Required]
        [Display(Name = "Label_Cost", ResourceType = typeof(Resource))]
        public decimal Cost { get; set; }

        [Required]
        [Display(Name = "Label_Count", ResourceType = typeof(Resource))]
        public int Count { get; set; }

        [Required]
        [Display(Name = "Label_Ord", ResourceType = typeof(Resource))]
        public int Ord { get; set; }
               
    }
}