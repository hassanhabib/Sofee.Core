﻿// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using Microsoft.Data.SqlClient;
using Sofee.Core.Api.Models.Countries;
using Sofee.Core.Api.Models.Countries.Exceptions;
using System;
using System.Threading.Tasks;
using Xeptions;

namespace Sofee.Core.Api.Services.Foundations.Countries
{
    public partial class CountryService
    {
        private delegate ValueTask<Country> ReturningCountryFunction();

        private async ValueTask<Country> TryCatch(ReturningCountryFunction returningCountryFunction)
        {
            try
            {
                return await returningCountryFunction();
            }
            catch (NullCountryException nullCountryException)
            {
                throw CretateAndLogValidationException(nullCountryException);
            }
            catch (InvalidCountryException invalidCountryException)
            {
                throw CretateAndLogValidationException(invalidCountryException);
            }
            catch (SqlException sqlException)
            {
                var failedCountryStorageException = new FailedCountryStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedCountryStorageException);
            }
        }

        private CountryValidationException CretateAndLogValidationException(Xeption exception)
        {
            var countryValidationException = new CountryValidationException(exception);
            this.loggingBroker.LogError(countryValidationException);

            return countryValidationException;
        }

        private CountryDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var countryDependencyException = new CountryDependencyException(exception);
            this.loggingBroker.LogCritical(countryDependencyException);

            return countryDependencyException;
        }
    }
}
