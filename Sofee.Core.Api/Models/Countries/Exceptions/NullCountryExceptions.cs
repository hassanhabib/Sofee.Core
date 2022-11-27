// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using Xeptions;

namespace Sofee.Core.Api.Models.Countries.Exceptions
{
    public class Xeption : Xeptions.Xeption
    {
        public Xeption()
       : base(message: "Country is null.")
        { }
    }
}
