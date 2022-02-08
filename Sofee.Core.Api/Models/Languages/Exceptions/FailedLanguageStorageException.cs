// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System;
using Xeptions;

namespace Sofee.Core.Api.Models.Languages.Exceptions
{
    public class FailedLanguageStorageException : Xeption
    {
        public FailedLanguageStorageException(Exception innerException)
            : base(message: "Failed language storage error occurred, contact support.", innerException)
        { }
    }
}
