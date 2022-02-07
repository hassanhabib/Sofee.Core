// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System;
using Sofee.Core.Api.Models.Languages;
using Sofee.Core.Api.Models.Languages.Exceptions;

namespace Sofee.Core.Api.Services.Foundations.Languages
{
    public partial class LanguageService
    {
        private void ValidateLanguageOnAdd(Language language)
        {
            ValidateLanguageIsNotNull(language);

            Validate(
                (Rule: IsInvalid(language.Id), Parameter: nameof(Language.Id)),
                (Rule: IsInvalid(language.ISO), Parameter: nameof(Language.ISO)),
                (Rule: IsInvalid(language.Text), Parameter: nameof(Language.Text)),
                (Rule: IsInvalid(language.CreatedDate), Parameter: nameof(Language.CreatedDate)),
                (Rule: IsInvalid(language.CreatedBy), Parameter: nameof(Language.CreatedBy)),
                (Rule: IsInvalid(language.UpdatedDate), Parameter: nameof(Language.UpdatedDate)),
                (Rule: IsInvalid(language.UpdatedBy), Parameter: nameof(Language.UpdatedBy)));
        }

        private static void ValidateLanguageIsNotNull(Language language)
        {
            if (language is null)
            {
                throw new NullLanguageException();
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidLanguageException =
                new InvalidLanguageException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidLanguageException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidLanguageException.ThrowIfContainsErrors();
        }
    }
}
