﻿using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface ICategoryRepository : IRepository<Categories>
    {
        Task<List<Categories>> GetAllCategories();
        Task<Categories> GetCategoryById(int categoryId); 
    }
}
