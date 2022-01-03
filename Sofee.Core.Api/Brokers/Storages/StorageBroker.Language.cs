// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Hosting;
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

        public IQueryable<Language> SelectAllLanguages()
        {
            using var broker =  
                new StorageBroker(this.configuration);

            return broker.Languages;
        }

        public async ValueTask<Language> SelectLanguageByIdAsync(Guid languageId)
        {
            using var broker = 
                   new StorageBroker(this.configuration);

            return await broker.Languages.FindAsync(languageId);
        }

        public async ValueTask<Language> UpdateLanguageAsync(Language language)
        {
            using var broker =
                new StorageBroker(this.configuration);

            EntityEntry<Language> languageEntityEntry =
                broker.Languages.Update(language);

            await broker.SaveChangesAsync();

            return languageEntityEntry.Entity;
        }

        public async ValueTask<Language> DeleteLanguageAsync(Language language)
        {
            using var broker = 
                new StorageBroker(this.configuration);

            EntityEntry<Language> languageEntityEntry = 
                broker.Languages.Remove(language);

            await broker.SaveChangesAsync();

            return languageEntityEntry.Entity;
        } 
    }
}
