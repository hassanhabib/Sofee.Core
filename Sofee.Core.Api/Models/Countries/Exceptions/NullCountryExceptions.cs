// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using Xeptions;

namespace Sofee.Core.Api.Models.Countries.Exceptions
{
    public class NullCountryException :Xeption
    {
        public NullCountryException()
       : base(message: "Country is null.")
        { }
    }
}
