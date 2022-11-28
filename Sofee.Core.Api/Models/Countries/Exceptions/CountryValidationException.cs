// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using Xeptions;

namespace Sofee.Core.Api.Models.Countries.Exceptions
{
    public class CountryValidationException :Xeption
    {
        public CountryValidationException(Xeption innerException)
        : base(message: "Country validation error occured. Please, try again.",innerException)
        { }
    }
}
