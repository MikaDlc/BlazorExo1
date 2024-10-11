using DemoWASM.Models;

namespace DemoWASM.Services
{
    public interface IGameService
    {
        List<Game> GetAll();
        //Gamer GetById(int id);
        void Save(Game gamer);
        void Update(Game gamer);
        void Delete(int id);
    }
}
