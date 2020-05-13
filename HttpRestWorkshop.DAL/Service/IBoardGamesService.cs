using System.Collections.Generic;
using HttpRestWorkshop.DAL.Models;

namespace HttpRestWorkshop.DAL.Service
{
    public interface IBoardGamesService
    {
        IEnumerable<BoardGame> Get();

        BoardGame Get(int id);

        void Add(BoardGame game);

        void Delete(int id);
    }
}
