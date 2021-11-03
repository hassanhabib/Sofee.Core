// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Sofee.Core.Api.Tests.Acceptance.Apis.Home
{
    public partial class HomeApiTests
    {
        [Fact]
        public async Task ShouldReturnHomeMessageAsync()
        {
            // given
            string expectedHomeMessage =
                "Hello Mario, the princess is in another castle.";

            // when
            string actualHomeMessage =
                await this.sofeeApiBroker.GetHomeMessageAsync();

            // then
            actualHomeMessage.Should().BeEquivalentTo(expectedHomeMessage);
        }
    }
}
