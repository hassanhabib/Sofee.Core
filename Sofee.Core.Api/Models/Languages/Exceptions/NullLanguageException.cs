// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------


using Xeptions;

namespace Sofee.Core.Api.Models.Languages.Exceptions
{
    public class NullLanguageException : Xeption
    {
        public NullLanguageException()
            : base(message: "Language is null.")
        {

        }
    }
}
