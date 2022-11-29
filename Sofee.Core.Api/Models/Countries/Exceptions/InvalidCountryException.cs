// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using Xeptions;

namespace Sofee.Core.Api.Models.Countries.Exceptions
{
    public class InvalidCountryException : Xeption
    {
        public InvalidCountryException()
            : base(message: "Invalid country. Please correct the errors and try again.")
        { }
    }
}
