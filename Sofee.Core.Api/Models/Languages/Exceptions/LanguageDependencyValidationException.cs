// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using Xeptions;

namespace Sofee.Core.Api.Models.Languages.Exceptions
{
    public class LanguageDependencyValidationException : Xeption
    {
        public LanguageDependencyValidationException(Xeption innerException)
            : base(message: "Language dependency validation occurred, please try again.", innerException)
        { }
    }
}
