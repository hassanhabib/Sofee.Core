// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using Xeptions;

namespace Sofee.Core.Api.Models.Languages.Exceptions
{
    public class LanguageDependencyException : Xeption
    {
        public LanguageDependencyException(Xeption innerException)
            : base(message: "Language dependency error occurred, contact support.", innerException)
        { }
    }
}
