namespace GameZone.Services
{
    public interface IGameService
    {
        Task Create(CreateGameFormViewModel mdoel);
        IEnumerable<Game> GetAll();
        Game? GetById(int id);
        Task<Game?> Edit(EditGameFormViewModel model);
        bool Delete(int id);
    }
}
