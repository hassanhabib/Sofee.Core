// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using Xeptions;

namespace Sofee.Core.Api.Models.Languages.Exceptions
{
    public class LanguageServiceException : Xeption
    {
        public LanguageServiceException(Xeption innerException)
            : base(message: "Language service error occurred, please contact support.", innerException)
        {

        }
    }
}
