// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System;
using Xeptions;

namespace Sofee.Core.Api.Models.Languages.Exceptions
{
    public class FailedLanguageServiceException : Xeption
    {
        public FailedLanguageServiceException(Exception innerException)
            : base(message: "Failed language service occurred, please contact support.", innerException)
        { }
    }
}
