using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcFreelan.Models.Mypays
{
    public class Mypay
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter value higher than zero.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Type Valid Field")]
        [Column(TypeName = "decimal(18, 4)")]
        public decimal TotalMypay { get; set; }
        public string DateCovered { get; set; }
        public string DateAdded { get; set; }
        public string? Note { get; set; }
        public string AspUser { get; set; }
        public bool Active { get; set; }

        public int SelectedMypaytypeId { get; set; }

        [NotMapped]
        public List<SelectListItem>? Items { get; set; }
    }
}
