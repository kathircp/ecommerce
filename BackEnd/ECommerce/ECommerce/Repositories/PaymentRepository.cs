using System;
using System.Collections.Generic;
using System.Linq;
using ECommerce.Models;
using ECommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories
{
    public interface IPaymentRepository
    {
        IEnumerable<Payment> GetAll();

        Payment? Get(int id);

        bool Create(Payment payment);
    }
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ECommerceDbContext _db;

        public PaymentRepository(ECommerceDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Payment> GetAll()
        {
            return _db.Payments.ToList();
        }

        public Payment? Get(int id)
        {

            return _db.Payments.Find(id);
        }

        public bool Create(Payment payment)
        {
            payment.CreatedAt = DateTime.UtcNow;
            _db.Payments.Add(payment);
            int count = _db.SaveChanges();
            return count > 0 ? true : false;
        }
    }
}