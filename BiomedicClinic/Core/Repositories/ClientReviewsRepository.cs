using BiomedicClinic.Core.Interfaces;
using BiomedicClinic.Core.Models;
using BiomedicClinic.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BiomedicClinic.Core.Repositories
{
    public class ClientReviewsRepository : IClientReviewsRepository
    {
        private readonly ApplicationDbContext _ctx;

        public ClientReviewsRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public void AddReview(ClientReview review)
        {
            _ctx.ClientReviews.Add(review);
        }

        public IEnumerable<ClientReview> GetActiveReviews()
        {
            return _ctx.ClientReviews.Include(cr => cr.ClientReviewType).Where(r => r.isActive);
        }

        public IEnumerable<ClientReview> GetAllReviews()
        {
            return _ctx.ClientReviews.Include(cr => cr.ClientReviewType);
        }

        public IEnumerable<ClientReview> GetInactiveReviews()
        {
            return _ctx.ClientReviews.Where(r => !r.isActive);
        }

        public ClientReview GetReviewById(int id)
        {
            return _ctx.ClientReviews.Include(cr => cr.ClientReviewType).Where(cr => cr.Id == id).FirstOrDefault();
        }

        public void RemoveReview(int id)
        {
            var review = _ctx.ClientReviews.Find(id);
            _ctx.ClientReviews.Remove(review);
        }
    }
}