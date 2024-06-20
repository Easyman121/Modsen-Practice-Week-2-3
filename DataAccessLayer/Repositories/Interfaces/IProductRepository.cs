﻿using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Products>
    {
        Task<List<Products>> GetProductsByCategoryAsync(int categoryId, CancellationToken cancellationToken);
    }
}
