// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System;
using Xeptions;

namespace Sofee.Core.Api.Models.Languages.Exceptions
{
    public class AlreadyExistsLanguageException : Xeption
    {
        public AlreadyExistsLanguageException(Exception innerException)
            : base(message: "Language with the same id already exists.", innerException)
        { }
    }
}
