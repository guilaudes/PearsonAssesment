using EFCoreInMemoryDbDemo;
using PearsonAssesment.Models;
using PearsonAssesment.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PearsonAssesment.Repositories
{
    internal class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository()
        {
        }
        
    }
}
