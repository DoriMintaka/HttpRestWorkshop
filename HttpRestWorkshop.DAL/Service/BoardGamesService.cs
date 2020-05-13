using System;
using System.Collections.Generic;
using System.Linq;
using HttpRestWorkshop.DAL.Models;
using Microsoft.Extensions.Caching.Memory;

namespace HttpRestWorkshop.DAL.Service
{
    public class BoardGamesService : IBoardGamesService
    {
        private readonly AppDbContext context;

        private readonly IMemoryCache cache;

        public BoardGamesService(AppDbContext context, IMemoryCache cache)
        {
            this.context = context;
            this.cache = cache;
        }

        public IEnumerable<BoardGame> Get()
        {
            return context.BoardGames.ToList();
        }

        public BoardGame Get(int id)
        {
            if (cache.TryGetValue(id, out BoardGame item))
            {
                return item;
            }
             
            item = context.BoardGames.SingleOrDefault(i => i.Id == id);

            if (item == null)
            {
                throw new ArgumentException();
            }

            cache.Set(id, item);
            return item;
        }

        public void Add(BoardGame game)
        {
            context.BoardGames.Add(game);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = context.BoardGames.SingleOrDefault(i => i.Id == id);
            if (item == null)
            {
                throw new ArgumentException();
            }

            context.BoardGames.Remove(item);
            cache.Remove(id);

            context.SaveChanges();
        }
    }
}
