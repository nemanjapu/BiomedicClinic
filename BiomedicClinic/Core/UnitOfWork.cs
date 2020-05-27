using BiomedicClinic.Core.Interfaces;
using BiomedicClinic.Core.Repositories;
using BiomedicClinic.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiomedicClinic.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _ctx;

        public IClientReviewsRepository ClientReviews { get; private set; }
        public ILeadsRepository Leads { get; private set; }
        public IMenusRepository Menus { get; private set; }
        public IWebsitePagesRepository WebsitePages { get; private set; }
        public IGlobalSettingsRepository GlobalSettings { get; private set; }

        public UnitOfWork(ApplicationDbContext ctx)
        {
            _ctx = ctx;
            ClientReviews = new ClientReviewsRepository(_ctx);
            Leads = new LeadsRepository(_ctx);
            Menus = new MenusRepository(_ctx);
            WebsitePages = new WebsitePagesRepository(_ctx);
            GlobalSettings = new GlobalValuesRepository(_ctx);
        }

        public void Complete()
        {
            _ctx.SaveChanges();
        }
    }
}