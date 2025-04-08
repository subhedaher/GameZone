using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly AppDbContext _appDbContext;

        public DeviceService(AppDbContext _appDbContext)
        {
            this._appDbContext = _appDbContext;
        }

        public IEnumerable<SelectListItem> GetSelectList()
        {
            return _appDbContext.Devices.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .OrderBy(c => c.Text).AsNoTracking().ToList();
        }
    }
}
