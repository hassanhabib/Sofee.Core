// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using Xeptions;

namespace Sofee.Core.Api.Models.Countries.Exceptions
{
    public class CountryDependencyException : Xeption
    {
        public CountryDependencyException(Xeption innerException)
        : base(message: "Country dependency error occurred, contact support.", innerException)
        { }
    }
}
