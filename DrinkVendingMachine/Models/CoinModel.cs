using DrinkVendingMachine.DTO;
using DrinkVendingMachine.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DrinkVendingMachine.Models
{
    public class CoinModel
    {
        public long Id { get; set; }

        [Required]
        [Display(Name = "Label_Denomination", ResourceType = typeof(Resource))]
        public int Denomination { get; set; }

        [Required]
        [Display(Name = "Label_Lock", ResourceType = typeof(Resource))]
        public bool Lock { get; set; }

        [Required]
        [Display(Name = "Label_Count", ResourceType = typeof(Resource))]
        public int Count { get; set; }

    }
}