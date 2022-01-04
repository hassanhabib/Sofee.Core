// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sofee.Core.Api.Models.Languages;
using Sofee.Core.Api.Models.Languages.Exceptions;
using Xunit;

namespace Sofee.Core.Api.Tests.Unit.Services.Foundations
{
    public partial class LanguageServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfLanguageIsNullAsync()
        {
            //given 
            Language nullLanguage = null;

            var nullLanguageException =
                    new NullLanguageException();

            var expectedLanguageValidationException =
                    new LanguageValidationException(nullLanguageException);

            //when
            ValueTask<Language> addLanguageTask =
                 this.languageService.AddLanguageAsync(nullLanguage);

            //then
            await Assert.ThrowsAsync<LanguageValidationException>(() =>
                addLanguageTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                   broker.InsertLanguageAsync(nullLanguage),
                       Times.Never);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
