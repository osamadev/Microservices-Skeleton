using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actio.Services.Activities.Domain.Models;

namespace Actio.Services.Activities.Domain.Repositories
{
    public interface ICategoryRepository
    {
         Task<Category> GetAsync(Guid categoryId);
        Task<Category> GetAsync(string name);
         Task<IEnumerable<Category>> GetAllAsync();
         Task AddAsync(Category category);
         
    }
}