// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using Xeptions;

namespace Sofee.Core.Api.Models.Languages.Exceptions
{
    public class InvalidLanguageException : Xeption
    {
        public InvalidLanguageException()
            : base(message: "Invalid language. Please correct the errors and try again.")
        { }
    }
}
