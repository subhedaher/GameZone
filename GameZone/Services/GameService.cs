namespace GameZone.Services
{
    public class GameService : IGameService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IFileService fileService;

        public GameService(AppDbContext context, IFileService fileService)
        {
            _appDbContext = context;
            this.fileService = fileService;
        }

        public async Task Create(CreateGameFormViewModel model)
        {
            Game game = new Game()
            {
                Name = model.Name,
                CategoryId = model.CategoryId,
                Description = model.Description,
                Cover = await fileService.SaveImage(model.Cover, "games"),
                Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList(),
            };

            _appDbContext.Games.Add(game);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Game> GetAll()
        {
            return _appDbContext.Games.AsNoTracking()
                .Include(g => g.Category)
                .Include(g => g.Devices).ThenInclude(s => s.Device).ToList();
        }

        public Game? GetById(int id)
        {
            return _appDbContext.Games.AsNoTracking()
                .Include(g => g.Category)
                .Include(g => g.Devices).ThenInclude(s => s.Device).FirstOrDefault(g => g.Id == id);
        }

        public async Task<Game?> Edit(EditGameFormViewModel model)
        {
            var game = _appDbContext.Games
                .Include(g => g.Devices)
                .SingleOrDefault(g => g.Id == model.Id);

            if (game is null) return null;

            var hasNewCover = model.Cover is not null;
            var oldCover = game.Cover;

            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();

            if (hasNewCover)
            {
                game.Cover = await fileService.SaveImage(model.Cover!, "games");
            }

            _appDbContext.Update(game);

            var effectedRows = _appDbContext.SaveChanges();

            if (effectedRows > 0)
            {
                if (hasNewCover)
                {
                    fileService.DeleteImage(oldCover);
                }

                return game;
            }
            else
            {
                fileService.DeleteImage(oldCover);
                return null;
            }
        }

        public bool Delete(int id)
        {
            var game = _appDbContext.Games.FirstOrDefault(g => g.Id == id);
            if (game is null) return false;

            var cover = game.Cover;
            _appDbContext.Games.Remove(game);
            var effectedRows = _appDbContext.SaveChanges();

            if (effectedRows > 0)
            {
                fileService.DeleteImage(cover);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
