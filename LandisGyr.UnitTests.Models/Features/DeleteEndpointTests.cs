using AutoFixture.Xunit2;
using LandisGyr.UnitTests.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using LandisGyr.ConsoleApp.Features;
using FluentAssertions;
using LandisGyr.ConsoleApp;
using LandisGyr.ConsoleApp.Models;

namespace LandisGyr.UnitTests.Features
{
    public class DeleteEndpointTests
    {
        [Trait(TraitsConstants.Category.Name, TraitsConstants.Category.Values.Unit)]
        public class DeleteEndpointHandlerTests
        {
            [Theory, CreateDataAttributes]
            [Trait(TraitsConstants.Label.Name, TraitsConstants.Label.Values.Feature)]
            public async Task Handle_NullRequest_ThrowsArgumentNullException(DeleteEndpointHandler subject)
            {
                // Arrange

                // Act
                Func<Task> act = () => subject.Handle(null, default);

                // Assert
                await act.Should().ThrowExactlyAsync<ArgumentNullException>();
            }

            [Theory, CreateDataAttributes]
            [Trait(TraitsConstants.Label.Name, TraitsConstants.Label.Values.Feature)]
            public async Task Handle_EndpointNotFound_ThrowsKeyNotFoundException(DeleteEndpoint request, DeleteEndpointHandler subject)
            {
                // Arrange

                // Act
                Func<Task> act = () => subject.Handle(request, default);

                // Assert
                await act.Should().ThrowExactlyAsync<KeyNotFoundException>();
            }

            [Theory, CreateDataAttributes]
            [Trait(TraitsConstants.Label.Name, TraitsConstants.Label.Values.Feature)]
            public async Task Handle_EndpointExists_ReturnsUnit(
                [Frozen] LandisGyrContext context,
                Endpoint endpoint,
                DeleteEndpointHandler subject)
            {
                // Arrange
                context.Endpoints.Add(endpoint);
                await context.SaveChangesAsync();

                // Act
                var result = await subject.Handle(new DeleteEndpoint { SerialNumber = endpoint.SerialNumber }, default);

                // Assert
                result.Should().NotBeNull();
                context.Endpoints.Should().BeEmpty();
            }
        }
    }
}
