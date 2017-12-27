using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore_WebForms.Models.Repository
{
    public class Repository
    {

        private EFDbContext context = new EFDbContext();

        public IEnumerable<Game> Games
        {
            get { return context.Games; }
        }
    }
}