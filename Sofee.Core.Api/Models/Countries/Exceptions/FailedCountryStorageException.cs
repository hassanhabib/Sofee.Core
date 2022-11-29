// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System;
using Xeptions;

namespace Sofee.Core.Api.Models.Countries.Exceptions
{
    public class FailedCountryStorageException:Xeption
    {
        public FailedCountryStorageException(Exception innerException)
        :base(message: "Failed country storage error occurred, contact support.",innerException) {}
    }
}
