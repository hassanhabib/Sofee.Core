using System;

namespace Sofee.Core.Api.Models.Languages
{
    public class Language
    {
        public Guid Id { get; set; }
        public string ISO { get; set; }
        public string Text { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}
