using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _appDbContext;

        public CategoryService(AppDbContext _appDbContext)
        {
            this._appDbContext = _appDbContext;
        }

        public IEnumerable<SelectListItem> GetSelectList()
        {
            return _appDbContext.Categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .OrderBy(c => c.Text).AsNoTracking().ToList();
        }
    }
}
