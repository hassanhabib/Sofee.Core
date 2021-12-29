// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sofee.Core.Api.Models.Languages;

namespace Sofee.Core.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Language> Languages { get; set; }

        public async ValueTask<Language> InsertLanguageAsync(Language language)
        {
            using var broker = new StorageBroker(this.configuration);

            EntityEntry<Language> languageEntityEntry =
                await broker.Languages.AddAsync(language);

            await broker.SaveChangesAsync();

            return languageEntityEntry.Entity; 
        }
    }
}
