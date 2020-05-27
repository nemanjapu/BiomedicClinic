using BiomedicClinic.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiomedicClinic.Core.Interfaces
{
    public interface IClientReviewsRepository
    {
        IEnumerable<ClientReview> GetAllReviews();
        IEnumerable<ClientReview> GetActiveReviews();
        IEnumerable<ClientReview> GetInactiveReviews();
        ClientReview GetReviewById(int id);
        void AddReview(ClientReview review);
        void RemoveReview(int id);
    }
}
