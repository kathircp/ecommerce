using System;
using System.Collections.Generic;
using System.Linq;
using ECommerce.Models;
using ECommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories
{
    public interface IShipmentRepository
    {
        IEnumerable<Shipment> GetAll();

        Shipment? Get(int id);

        Shipment Create(Shipment order);
    }
    public class ShipmentRepository : IShipmentRepository
    {
        private readonly ECommerceDbContext _db;

        public ShipmentRepository(ECommerceDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Shipment> GetAll()
        {
            return _db.Shipments.ToList();
        }

        public Shipment? Get(int id)
        {

            return _db.Shipments.Find(id);
        }

        public Shipment Create(Shipment shipment)
        {
            shipment.CreatedAt = DateTime.UtcNow;
            _db.Shipments.Add(shipment);
            _db.SaveChanges();
            return shipment;
        }
    }
}