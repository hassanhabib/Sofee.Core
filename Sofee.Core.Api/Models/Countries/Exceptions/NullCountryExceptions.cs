// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using Xeptions;

namespace Sofee.Core.Api.Models.Countries.Exceptions
{
    public class NullCountryExceptions : Xeption
    {
        public NullCountryExceptions()
       : base(message: "Country is null.")
        { }
    }
}
