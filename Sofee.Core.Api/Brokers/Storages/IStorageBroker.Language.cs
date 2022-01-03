// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Sofee.Core.Api.Models.Languages;

namespace Sofee.Core.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Language> InsertLanguageAsync(Language language);
        IQueryable<Language> SelectAllLanguages();
        ValueTask<Language> UpdateLanguageAsync(Language language);
    }
}
