using AutoFixture.Xunit2;
using FluentAssertions;
using LandisGyr.ConsoleApp;
using LandisGyr.ConsoleApp.Features;
using LandisGyr.ConsoleApp.Models;
using LandisGyr.UnitTests.Utils;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LandisGyr.UnitTests.Features
{
    public class FindEndpointTests
    {
        [Trait(TraitsConstants.Category.Name, TraitsConstants.Category.Values.Unit)]
        public class FindEndpointsHandlerTests
        {
            [Theory]
            [CreateDataAttributes]
            [Trait(TraitsConstants.Label.Name, TraitsConstants.Label.Values.Feature)]
            public async Task Handle_NullRequest_ThrowsArgumentNull(FindEndpointHandler subject)
            {
                // Arrange            

                // Act
                Func<Task> act = () => subject.Handle(null, default);

                // Assert
                await act
                    .Should()
                    .ThrowExactlyAsync<ArgumentNullException>();
            }

            [Theory]
            [CreateDataAttributes]
            [Trait(TraitsConstants.Label.Name, TraitsConstants.Label.Values.Feature)]
            public async Task Handle_EndpointNotFound_ReturnsNull([Frozen] LandisGyrContext context,
                Endpoint endpoint,
                FindEndpointHandler subject)
            {
                // Arrange
                context.Add(endpoint);
                await context.SaveChangesAsync();

                // Act
                var result = await subject.Handle(new FindEndpoint { SerialNumber = string.Empty }, default);

                // Assert
                result
                    .Should()
                    .BeNull();
            }

            [Theory]
            [CreateDataAttributes]
            [Trait(TraitsConstants.Label.Name, TraitsConstants.Label.Values.Feature)]
            public async Task Handle_ExistingContext_ReturnsEndpoint([Frozen] LandisGyrContext context,
                Endpoint endpoint,
                FindEndpointHandler subject)
            {
                // Arrange
                context.Add(endpoint);
                await context.SaveChangesAsync();

                // Act
                var result = await subject.Handle(new FindEndpoint {SerialNumber = endpoint.SerialNumber }, default);

                // Assert
                result
                    .Should()
                    .NotBeNull().And
                    .BeEquivalentTo(endpoint);
            }
        }
    }
}
