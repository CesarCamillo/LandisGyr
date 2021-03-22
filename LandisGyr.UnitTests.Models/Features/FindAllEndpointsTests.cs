using AutoFixture.Xunit2;
using FluentAssertions;
using LandisGyr.ConsoleApp;
using LandisGyr.ConsoleApp.Features;
using LandisGyr.ConsoleApp.Models;
using LandisGyr.UnitTests.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LandisGyr.UnitTests.Features
{
    public class FindAllEndpointsTests
    {
        [Trait(TraitsConstants.Category.Name, TraitsConstants.Category.Values.Unit)]
        public class ListAllEndpointsHandlerTests
        {
            [Theory]
            [CreateDataAttributes]
            [Trait(TraitsConstants.Label.Name, TraitsConstants.Label.Values.Feature)]
            public async Task Handle_NullRequest_ThrowsArgumentNull(FindAllEndpointsHandler subject)
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
            public async Task Handle_EmptyContext_ReturnsEmptyList(FindAllEndpointsHandler subject)
            {
                // Arrange

                // Act
                var result = await subject.Handle(new FindAllEndpoints(), default);

                // Assert
                result
                    .Should()
                    .NotBeNull().And
                    .BeEmpty();
            }

            [Theory]
            [CreateDataAttributes]
            [Trait(TraitsConstants.Label.Name, TraitsConstants.Label.Values.Feature)]
            public async Task Handle_ExistingContext_ReturnsList([Frozen] LandisGyrContext context,
                IEnumerable<Endpoint> endpoints,
                FindAllEndpointsHandler subject)
            {
                // Arrange
                context.AddRange(endpoints);
                await context.SaveChangesAsync();

                // Act
                var result = await subject.Handle(new FindAllEndpoints(), default);

                // Assert
                result
                    .Should()
                    .NotBeNull().And
                    .HaveSameCount(context.Endpoints);
            }
        }
    }
}
