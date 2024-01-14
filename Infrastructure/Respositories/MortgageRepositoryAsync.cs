using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Respositories
{
    public class MortgageRepositoryAsync : GenericRepositoryAsync<Mortgage>, IMortgageRepositoryAsync
    {
        private readonly DbSet<Mortgage> _mortgage;
        private readonly DbSet<Customer> _customer;
        private readonly DbSet<Property> _property;

        public MortgageRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _mortgage = dbContext.Set<Mortgage>();
            _customer = dbContext.Set<Customer>();
            _property = dbContext.Set<Property>();

        }
       
    }
}
