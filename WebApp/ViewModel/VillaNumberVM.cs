using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Domain.Entites;

namespace WebApp.Web.ViewModel
{
    public class VillaNumberVM
    {
        public VillaNumber? VillaNumber { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? VillaList   { get; set; }


    }
}
