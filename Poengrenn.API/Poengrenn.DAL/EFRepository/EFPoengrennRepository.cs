using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poengrenn.DAL.Models;

namespace Poengrenn.DAL.EFRepository
{
    public class EFPoengrennRepository<TEntity> : BaseRepository<TEntity> where TEntity : class
    {
        // This class is just for setting up the correct DbContext

        public EFPoengrennRepository() : base(new PoengrennContext())
        {

        }
    }
}
