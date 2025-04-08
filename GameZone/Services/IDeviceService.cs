using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Services
{
    public interface IDeviceService
    {
        IEnumerable<SelectListItem> GetSelectList();
    }
}
