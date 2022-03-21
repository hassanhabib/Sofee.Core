// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Sofee.Core.Api.Models.Countries;

namespace Sofee.Core.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Country> Countries { get; set; }
    }
}
