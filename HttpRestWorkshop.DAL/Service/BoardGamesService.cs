using System;
using System.Collections.Generic;

namespace HttpRestWorkshop.DAL.Service
{
    using System.Linq;

    using HttpRestWorkshop.DAL.Models;

    using Microsoft.Extensions.Caching.Memory;

    public class BoardGamesService : IDisposable
    {
        private readonly AppDbContext _context;

        private readonly IMemoryCache _cache;

        public BoardGamesService(AppDbContext context, IMemoryCache cache)
        {
            this._context = context;
            this._cache = cache;
        }

        public IEnumerable<BoardGame> Get()
        {
            return this._context.BoardGames.ToList();
        }

        public BoardGame Get(int id)
        {
            if (this._cache.TryGetValue(id, out BoardGame item))
            {
                return item;
            }
             
            item = this._context.BoardGames.SingleOrDefault(i => i.Id == id);

            if (item == null)
            {
                throw new ArgumentException();
            }

            this._cache.Set(id, item);
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
            this._cache.Remove(id);

            this._context.SaveChanges();
        }

        public void Dispose()
        {
            this._context.Dispose();
        }
    }
}
