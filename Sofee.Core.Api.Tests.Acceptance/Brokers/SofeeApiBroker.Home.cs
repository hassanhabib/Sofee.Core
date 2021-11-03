// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System.Threading.Tasks;

namespace Sofee.Core.Api.Tests.Acceptance.Brokers
{
    public partial class SofeeApiBroker
    {
        private const string HomeRelativeUrl = "api/home";

        public async ValueTask<string> GetHomeMessageAsync() =>
            await this.apiFactoryClient.GetContentStringAsync(HomeRelativeUrl);
    }
}
