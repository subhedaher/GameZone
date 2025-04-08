using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Services
{
    public interface ICategoryService
    {
        IEnumerable<SelectListItem> GetSelectList();
    }
}
