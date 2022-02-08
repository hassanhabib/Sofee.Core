// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System;
using Xeptions;

namespace Sofee.Core.Api.Models.Languages.Exceptions
{
    public class InvalidLanguageReferenceException : Xeption
    {
        public InvalidLanguageReferenceException(Exception innerException)
            : base(message: "Invalid language reference error occurred.", innerException)
        { }
    }
}
