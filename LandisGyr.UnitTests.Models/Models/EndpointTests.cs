using LandisGyr.ConsoleApp.Models;
using LandisGyr.UnitTests.Utils;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using AutoFixture.Xunit2;
using FluentAssertions;

namespace LandisGyr.UnitTests.Models
{
    public class EndpointTests
    {
        [Trait(TraitsConstants.Category.Name, TraitsConstants.Category.Values.Unit)]
        public class EndpointQueriesTests
        {
            private readonly IEnumerable<Endpoint> _emptyResult;

            public EndpointQueriesTests()
            {
                _emptyResult = Enumerable.Empty<Endpoint>();
            }

            [Fact]
            [Trait(TraitsConstants.Label.Name, TraitsConstants.Label.Values.Domain)]
            public void AllEndpoints_EmptyData_ReturnsEmpty()
            {
                // Arrange
                var subject = _emptyResult.AsQueryable();

                // Act
                var result = subject.AllEndpoints();

                // Assert
                result.Should().BeEmpty();
            }

            [Theory, CreateDataAttributes]
            [Trait(TraitsConstants.Label.Name, TraitsConstants.Label.Values.Domain)]
            public void AllEndpoints_WithData_ReturnsData(IEnumerable<Endpoint> Endpoints)
            {
                // Arrange
                var subject = Endpoints.AsQueryable();

                // Act
                var result = subject.AllEndpoints();

                // Assert
                result.Should()
                    .NotBeEmpty().And
                    .HaveSameCount(Endpoints);
            }

            [Theory, CreateDataAttributes]
            [Trait(TraitsConstants.Label.Name, TraitsConstants.Label.Values.Domain)]
            public void BySerialNumber_EmptyData_ReturnsEmpty(string serial)
            {
                // Arrange
                var subject = _emptyResult.AsQueryable();

                // Act
                var result = subject.WithSerialNumber(serial);

                // Assert
                result.Should().BeEmpty();
                result.SingleOrDefault().Should().BeNull();
            }

            [Theory, CreateDataAttributes]
            [Trait(TraitsConstants.Label.Name, TraitsConstants.Label.Values.Domain)]
            public void BySerialNumber_WithDataAndValidSerialNumber_ReturnsData(IEnumerable<Endpoint> Endpoints, Endpoint extraEndpoint)
            {
                // Arrange
                var serialNumber = extraEndpoint.SerialNumber;
                var subject = Endpoints
                    .Concat(new List<Endpoint>() { extraEndpoint })
                    .AsQueryable();

                // Act
                var result = subject.WithSerialNumber(serialNumber);

                // Assert
                result.Should().NotBeEmpty();
                result.SingleOrDefault().Should()
                    .BeEquivalentTo(extraEndpoint);
            }

            [Theory, CreateDataAttributes]
            [Trait(TraitsConstants.Label.Name, TraitsConstants.Label.Values.Domain)]
            public void BySerialNumber_WithDataAndInvalidSerialNumber_ReturnsData(IEnumerable<Endpoint> Endpoints, [Frozen] int serialNumber)
            {
                // Arrange
                var realSerialNumber = serialNumber + "####";
                var subject = Endpoints.AsQueryable();

                // Act
                var result = subject
                    .WithSerialNumber(realSerialNumber);

                // Assert
                result.Should().BeEmpty();
                result.SingleOrDefault().Should().BeNull();
            }
        }
    }
}
