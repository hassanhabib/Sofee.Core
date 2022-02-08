// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
            catch (InvalidLanguageException invalidLanguageException)
            {
                throw CreateAndLogValidationException(invalidLanguageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsLanguageException =
                    new AlreadyExistsLanguageException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(alreadyExistsLanguageException);
            }
            catch (ForeignKeyConstraintConflictException foreignKeyConstraintConflictException)
            {
                var invalidLanguageReferenceException =
                    new InvalidLanguageReferenceException(foreignKeyConstraintConflictException);

                throw CreateAndLogDependencyValidationException(invalidLanguageReferenceException);
            }
            catch (DbUpdateException databaseUpdateException)
            {
                var failedStorageLanguageException =
                    new FailedLanguageStorageException(databaseUpdateException);

                throw CreateAndLogDependencyException(failedStorageLanguageException);
            }
            catch(Exception exception)
            {
                var failedLanguageServiceException =
                    new FailedLanguageServiceException(exception);

                throw CreateAndLogServiceException(failedLanguageServiceException);
            }
        }

        private LanguageServiceException CreateAndLogServiceException(Xeption exception)
        {
            var languageServiceException = 
                new LanguageServiceException(exception);

            this.loggingBroker.LogError(languageServiceException);

            return languageServiceException;
        }

        private LanguageDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var languageDependencyException = 
                new LanguageDependencyException(exception);

            this.loggingBroker.LogCritical(languageDependencyException);

            return languageDependencyException;
        }

        private LanguageValidationException CreateAndLogValidationException(Xeption exception)
        {
            var languageValidationException = 
                new LanguageValidationException(exception);

            this.loggingBroker.LogError(languageValidationException);

            return languageValidationException;
        }

        private LanguageDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var languageDependencyValidationException =
                new LanguageDependencyValidationException(exception);

            this.loggingBroker.LogError(languageDependencyValidationException);

            return languageDependencyValidationException;
        }

        private LanguageDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var languageDependencyException =
                new LanguageDependencyException(exception);

            this.loggingBroker.LogError(languageDependencyException);

            return languageDependencyException;
        }
    }
}
