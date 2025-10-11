using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;

namespace MvcFreelan.Models.Mypays
{
    [Index(nameof(MypayName), IsUnique = true)]
    public class MypayType
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        [Display(Name = "Payment Type Name")] // Sets the display name for this property
        public string MypayName { get; set; }

        public string Created { get; set; }
        public bool Active { get; set; }
    }
}
