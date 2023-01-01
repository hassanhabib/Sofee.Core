// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Sofee.Core.Api.Models.Languages;
using Sofee.Core.Api.Models.Languages.Exceptions;
using Sofee.Core.Api.Services.Foundations.Languages;

namespace Sofee.Core.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LanguagesController : RESTFulController
    {
        private readonly ILanguageService languageService;

        public LanguagesController(ILanguageService languageService) =>
        this.languageService = languageService;

        [HttpPost]
        public async ValueTask<ActionResult<Language>> LanguageLanguageAsync(Language language)
        {
            try
            {
                Language addedLanguage =
                    await this.languageService.AddLanguageAsync(language);

                return Created(addedLanguage);
            }
            catch (LanguageValidationException languageValidationException)
            {
                return BadRequest(languageValidationException.InnerException);
            }
            catch (LanguageDependencyValidationException languageDependencyValidationException)
               when (languageDependencyValidationException.InnerException is AlreadyExistsLanguageException)
            {
                return Conflict(languageDependencyValidationException.InnerException);
            }
            catch (LanguageDependencyException languageDependencyException)
            {
                return InternalServerError(languageDependencyException);
            }
            catch (LanguageServiceException languageServiceException)
            {
                return InternalServerError(languageServiceException);
            }
        }
    }
}
