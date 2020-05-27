using BiomedicClinic.Core.Interfaces;
using BiomedicClinic.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiomedicClinic.Core.Repositories
{
    public class ClientReviewTypesRepository : IClientReviewTypesRepository
    {
        private readonly ApplicationDbContext _ctx;

        public ClientReviewTypesRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
    }
}