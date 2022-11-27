// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using Sofee.Core.Api.Models.Countries.Exceptions;
using Sofee.Core.Api.Models.Countries;

namespace Sofee.Core.Api.Services.Foundations.Countries
{
    public partial class CountryService
    {
        private static void ValidateCountryNotNull(Country country)
        {
            if (country is null)
            {   
                throw new Xeption();
            }
        }
    }
}
