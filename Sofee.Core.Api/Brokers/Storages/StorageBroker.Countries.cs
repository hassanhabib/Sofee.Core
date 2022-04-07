// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sofee.Core.Api.Models.Countries;
using System.Threading.Tasks;

namespace Sofee.Core.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Country> Countries { get; set; }
        
        public async ValueTask<Country> InsertCounrtyAsync(Country country)
        {
            using var broker = new StorageBroker(this.configuration);

            EntityEntry<Country> countryEntityEntry =
                await broker.Countries.AddAsync(country);

            await broker.SaveChangesAsync();

            return countryEntityEntry.Entity;
        }
    }
}
