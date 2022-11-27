// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using Sofee.Core.Api.Brokers.Storages;
using Sofee.Core.Api.Models.Countries;
using System.Threading.Tasks;

namespace Sofee.Core.Api.Services.Foundations.Countries
{
    public interface ICountryService
    {
        ValueTask<Country> AddCountryAsync(Country country);
    }
}