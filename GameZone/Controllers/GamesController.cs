namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IDeviceService deviceService;
        private readonly IGameService gameService;

        public GamesController(ICategoryService categoryService, IDeviceService deviceService, IGameService gameService)
        {
            this.categoryService = categoryService;
            this.deviceService = deviceService;
            this.gameService = gameService;
        }
        public IActionResult Index()
        {
            var games = gameService.GetAll();
            return View(games);
        }

        public IActionResult Create()
        {
            CreateGameFormViewModel viewModel = new()
            {
                Categories = categoryService.GetSelectList(),
                Devices = deviceService.GetSelectList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = categoryService.GetSelectList();
                model.Devices = deviceService.GetSelectList();
                return View(model);
            }

            await gameService.Create(model);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var game = gameService.GetById(id);
            if (game is null)
                return NotFound();
            return View(game);
        }

        public IActionResult Edit(int id)
        {
            var game = gameService.GetById(id);
            if (game is null) return NotFound();

            EditGameFormViewModel viewModel = new()
            {
                Id = id,
                Name = game.Name,
                Description = game.Description,
                CategoryId = game.CategoryId,
                SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                Categories = categoryService.GetSelectList(),
                Devices = deviceService.GetSelectList(),
                CurrentCover = game.Cover,
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = categoryService.GetSelectList();
                model.Devices = deviceService.GetSelectList();
                return View(model);
            }

            var game = await gameService.Edit(model);

            if (game is null) return BadRequest();

            return RedirectToAction(nameof(Index));
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = gameService.Delete(id);
            if (isDeleted) return Ok();
            return BadRequest();
        }
    }
}
