// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using Sofee.Core.Api.Models.Languages;
using Sofee.Core.Api.Models.Languages.Exceptions;
using System.Threading.Tasks;
using Xeptions;

namespace Sofee.Core.Api.Services.Foundations.Languages
{
    public partial class LanguageService
    {
        private delegate ValueTask<Language> ReturningLanguageFunction();

        private async ValueTask<Language> TryCatch(ReturningLanguageFunction returningLanguageFunction)
        {
            try
            {
                return await returningLanguageFunction();
            }
            catch (NullLanguageException nullLanguageException)
            {
                throw CreateAndLogValidationException(nullLanguageException);
            }
        }

        private Xeption CreateAndLogValidationException(
            NullLanguageException nullLanguageException)
        {
            var languageValidationException = 
                new LanguageValidationException(nullLanguageException);

            this.loggingBroker.LogError(languageValidationException);

            return languageValidationException;
        }
    }
}
