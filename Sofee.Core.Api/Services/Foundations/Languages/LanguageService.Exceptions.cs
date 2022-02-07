// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using Microsoft.Data.SqlClient;
using Sofee.Core.Api.Models.Languages;
using Sofee.Core.Api.Models.Languages.Exceptions;
using System;
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
            catch (SqlException sqlException)
            {
                var failedLanguageStorageException =
                    new FailedLanguageStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedLanguageStorageException);
            }
        }

        private LanguageDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var languageDependencyException = 
                new LanguageDependencyException(exception);

            this.loggingBroker.LogCritical(languageDependencyException);

            return languageDependencyException;
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
