// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using Sofee.Core.Api.Models.Countries;
using Sofee.Core.Api.Models.Countries.Exceptions;
using System;

namespace Sofee.Core.Api.Services.Foundations.Countries
{
    public partial class CountryService
    {
        private static void ValidateCountry(Country country)
        {
            ValidateCountryNotNull(country);

            Validate(
                (Rule: IsInvalid(country.Id), Parameter: nameof(Country.Id)),
                (Rule: IsInvalid(country.Name), Parameter: nameof(Country.Name)),
                (Rule: IsInvalid(country.Iso3), Parameter: nameof(Country.Iso3)),
                (Rule: IsInvalid(country.CreatedDate), Parameter: nameof(Country.CreatedDate)),
                (Rule: IsInvalid(country.UpdatedDate), Parameter: nameof(Country.UpdatedDate)),
                (Rule: IsInvalid(country.CreatedBy), Parameter: nameof(Country.CreatedBy)),
                (Rule: IsInvalid(country.UpdatedBy), Parameter: nameof(Country.UpdatedBy)));
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == default,
            Message = "Id is required"
        };
        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };
        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static void ValidateCountryNotNull(Country country)
        {
            if (country is null)
            {
                throw new NullCountryException();
            }
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidCountryException = new InvalidCountryException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidCountryException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidCountryException.ThrowIfContainsErrors();
        }
    }
}
