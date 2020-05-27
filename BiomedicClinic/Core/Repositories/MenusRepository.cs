using BiomedicClinic.Core.Interfaces;
using BiomedicClinic.Core.Models;
using BiomedicClinic.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiomedicClinic.Core.Repositories
{
    public class MenusRepository : IMenusRepository
    {
        private readonly ApplicationDbContext _ctx;

        public MenusRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Menu> GetAllMenus()
        {
            return _ctx.Menus.AsEnumerable();
        }

        public Menu GetMenuById(int id)
        {
            return _ctx.Menus.Find(id);
        }
    }
}