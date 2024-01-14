using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Respositories
{
    public class PropertyRepositoryAsync : GenericRepositoryAsync<Property>, IPropertyRepositoryAsync
    {
        private readonly DbSet<Property> _property;

        public PropertyRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _property = dbContext.Set<Property>();
        }
    }
   

}
