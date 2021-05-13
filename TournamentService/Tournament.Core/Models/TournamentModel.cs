using AutoMapper;
using System;
using System.Collections.Generic;
using Entities = Tournament.Data.Entities;

namespace Tournament.Core.Models
{
    public class TournamentModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Date { get; set; }

        public int EntryPrice { get; set; }

        public string Address { get; set; }

        public class TournamentProfile : Profile
        {
            public TournamentProfile()
            {
                CreateMap<Entities.Tournament, TournamentModel>();
            }
        }
    }
}
