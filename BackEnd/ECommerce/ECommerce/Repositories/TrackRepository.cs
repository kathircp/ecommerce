using System;
using System.Collections.Generic;
using System.Linq;
using ECommerce.Models;
using ECommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories
{
    public interface ITrackRepository
    {
        IEnumerable<Track> GetAll();

        Track? Get(int id);

        Track Create(Track order);
    }
    public class TrackRepository : ITrackRepository
    {
        private readonly ECommerceDbContext _db;

        public TrackRepository(ECommerceDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Track> GetAll()
        {
            return _db.Tracks.ToList();
        }

        public Track? Get(int id)
        {

            return _db.Tracks.Find(id);
        }

        public Track Create(Track track)
        {            
            track.CreatedAt = DateTime.UtcNow;
            _db.Tracks.Add(track);
            _db.SaveChanges();
            return track;
        }
    }
}