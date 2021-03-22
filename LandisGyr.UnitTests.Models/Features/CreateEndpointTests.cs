using AutoFixture.Xunit2;
using FluentAssertions;
using LandisGyr.ConsoleApp;
using LandisGyr.ConsoleApp.Features;
using LandisGyr.UnitTests.Utils;
using System;
using System.Threading.Tasks;
using Xunit;
using FluentValidation.TestHelper;

namespace LandisGyr.UnitTests.Features
{
    public class CreateEndpointTests
    {
        [Trait(TraitsConstants.Category.Name, TraitsConstants.Category.Values.Unit)]
        public class CreateEndpointValidatorTests
        {
            [Theory, CreateDataAttributes]
            [Trait(TraitsConstants.Label.Name, TraitsConstants.Label.Values.Feature)]
            public void Validate_EmptySerial_Throws(CreateEndpointValidator subject)
            {
                // Arrange

                // Act
                var result = subject.TestValidate(new CreateEndpoint { SerialNumber = string.Empty });

                // Assert
                result.ShouldHaveValidationErrorFor(r => r.SerialNumber);
            }

            [Theory, CreateDataAttributes]
            [Trait(TraitsConstants.Label.Name, TraitsConstants.Label.Values.Feature)]
            public void Validate_Serial_Validates(CreateEndpoint command, CreateEndpointValidator subject)
            {
                // Arrange

                // Act
                var result = subject.TestValidate(command);

                // Assert
                result.ShouldNotHaveAnyValidationErrors();
            }
        }

        [Trait(TraitsConstants.Category.Name, TraitsConstants.Category.Values.Unit)]
        public class CreateEndpointHandlerTests
        {
            [Theory, CreateDataAttributes]
            [Trait(TraitsConstants.Label.Name, TraitsConstants.Label.Values.Feature)]
            public async Task Handle_NullCommand_ThrowsArgumentNullException(CreateEndpointHandler subject)
            {
                // Arrange

                // Act
                Func<Task> act = () => subject.Handle(null, default);

                // Assert
                await act.Should().ThrowExactlyAsync<ArgumentNullException>();
            }

            [Theory, CreateDataAttributes]
            [Trait(TraitsConstants.Label.Name, TraitsConstants.Label.Values.Feature)]
            public async Task Handle_ValidRequest_StoresIntoContext(
                [Frozen] LandisGyrContext context,
                CreateEndpoint command,
                CreateEndpointHandler subject
                )
            {
                // Arrange

                // Act
                var result = await subject.Handle(command, default);

                // Assert
                result.Should().NotBeNull();
                result.Should().BeEquivalentTo(command);
                context.Endpoints.Should().Contain(result);
            }
        }
    }
}
