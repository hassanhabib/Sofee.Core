// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System;

namespace Sofee.Core.Api.Models.Countries
{
    public class Country
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Iso3 { get; set; }
        public string Continent { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}
