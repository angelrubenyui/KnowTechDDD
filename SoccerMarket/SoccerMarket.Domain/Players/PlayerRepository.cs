﻿using Newtonsoft.Json;
using SoccerMarket.Domain.Players.Objects;
using SoccerMarket.Shared.Patterns.Repository;
using SoccerMarket.Shared.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerMarket.Domain.Players
{
    public sealed class PlayerRepository : IPlayerRepository
    {
        private readonly IDatabase _db = RedisConnectionFactory.GetDatabase();

        public HashSet<Player> GetPlayersByAgeRange(int minAge = 0, int maxAge = 40)
        {
            return null;
        }
             
        public void Add(Player domainObject)
        {
            domainObject.Id = Guid.NewGuid();

            string domainObjectJson = JsonConvert.SerializeObject(domainObject);

            _db.HashSet
            (
                "soccermarket:players", 
                new [] { new HashEntry(domainObject.Id.ToString(), domainObjectJson) }
            );
        }

        public Player GetById(Guid id)
        {
            string playerJson = _db.HashGet("soccermarket:players", id.ToString());

            return JsonConvert.DeserializeObject<Player>(playerJson);
        }

        public void Remove(Player domainObject)
        {
            throw new NotImplementedException();
        }
    }
}
