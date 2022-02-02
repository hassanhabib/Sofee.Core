// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using Sofee.Core.Api.Models.Languages;
using System.Threading.Tasks;

namespace Sofee.Core.Api.Services.Foundations.Languages
{
    public interface ILanguageService
    {
        ValueTask<Language> AddLanguageAsync(Language language);
    }
}
