// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using Sofee.Core.Api.Tests.Acceptance.Brokers;
using Xunit;

namespace Sofee.Core.Api.Tests.Acceptance.Apis.Home
{
    [Collection(nameof(ApiTestCollection))]
    public partial class HomeApiTests
    {
        private readonly SofeeApiBroker sofeeApiBroker;

        public HomeApiTests(SofeeApiBroker sofeeApiBroker) =>
            this.sofeeApiBroker = sofeeApiBroker;
    }
}
