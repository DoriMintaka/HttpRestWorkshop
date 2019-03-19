using System;
using System.Collections.Generic;

namespace HttpRestWorkshop.DAL.Service
{
    using System.Linq;

    using HttpRestWorkshop.DAL.Models;

    public class BoardGamesService : IDisposable
    {
        private readonly AppDbContext _context;

        public BoardGamesService(AppDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<BoardGame> Get()
        {
            return this._context.BoardGames.ToList();
        }

        public BoardGame Get(int id)
        {
            var item = this._context.BoardGames.SingleOrDefault(i => i.Id == id);

            if (item == null)
            {
                throw new ArgumentException();
            }

            return item;
        }

        public void Add(BoardGame game)
        {
            this._context.BoardGames.Add(game);
            this._context.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = this._context.BoardGames.SingleOrDefault(i => i.Id == id);
            if (item == null)
            {
                throw new ArgumentException();
            }

            this._context.BoardGames.Remove(item);

            this._context.SaveChanges();
        }

        public void Dispose()
        {
            this._context.Dispose();
        }
    }
}
