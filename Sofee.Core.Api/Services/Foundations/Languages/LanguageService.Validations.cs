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
                (Rule: IsInvalid(language.UpdatedBy), Parameter: nameof(Language.UpdatedBy)),
                (Rule: IsNotRecent(language.CreatedDate), Parameter: nameof(Language.CreatedDate)),

                (Rule: IsNotSame(
                    firstDate: language.UpdatedDate,
                    secondDate: language.CreatedDate,
                    secondDateName: nameof(Language.CreatedDate)),
                Parameter: nameof(Language.UpdatedDate)));
        }

        private void ValidateLanguageOnModify(Language language)
        {
            ValidateLanguageIsNotNull(language);

            Validate(
                (Rule: IsInvalid(language.Id), Parameter: nameof(Language.Id)),
                (Rule: IsInvalid(language.ISO), Parameter: nameof(Language.ISO)),
                (Rule: IsInvalid(language.Text), Parameter: nameof(Language.Text)),
                (Rule: IsInvalid(language.CreatedDate), Parameter: nameof(Language.CreatedDate)),
                (Rule: IsInvalid(language.CreatedBy), Parameter: nameof(Language.CreatedBy)),
                (Rule: IsInvalid(language.UpdatedDate), Parameter: nameof(Language.UpdatedDate)),
                (Rule: IsInvalid(language.UpdatedBy), Parameter: nameof(Language.UpdatedBy)),

                (Rule: IsSame(
                    firstDate: language.UpdatedDate,
                    secondDate: language.CreatedDate,
                    secondDateName: nameof(Language.CreatedDate)),
                Parameter: nameof(Language.UpdatedDate)),

            (Rule: IsNotRecent(language.UpdatedDate), Parameter: nameof(Language.UpdatedDate)));
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

        private static dynamic IsNotSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate != secondDate,
                Message = $"Date is not the same as {secondDateName}"
            };
        private static dynamic IsSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate == secondDate,
                Message = $"Date is the same as {secondDateName}"
            };

        private dynamic IsNotRecent(DateTimeOffset date) => new
        {
            Condition = IsDateNotRecent(date),
            Message = "Date is not recent"
        };

        private bool IsDateNotRecent(DateTimeOffset date)
        {
            DateTimeOffset currentDateTime =
                this.dateTimeBroker.GetCurrentDateTimeOffset();

            TimeSpan timeDifference = currentDateTime.Subtract(date);
            TimeSpan oneMinute = TimeSpan.FromMinutes(1);

            return timeDifference.Duration() > oneMinute;
        }

        private static void ValidateStorageLanguage(Language language, Guid languageId)
        {
            if (language is null)
                throw new NotFoundLanguageException(languageId);
        }

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
