// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System.Linq;
using Microsoft.EntityFrameworkCore;
using Sofee.Core.Api.Models.Languages;

namespace Sofee.Core.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Language> Languages { get; set; }
        public IQueryable<Language> SelectAllLanguages()
        {
            using var broker =  
                new StorageBroker(this.configuration);

            return broker.Languages;
        }
    }
}
