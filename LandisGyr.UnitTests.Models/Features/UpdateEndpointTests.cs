using AutoFixture.Xunit2;
using FluentAssertions;
using FluentValidation.TestHelper;
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
    public class UpdateEndpointTests
    {
        [Trait(TraitsConstants.Category.Name, TraitsConstants.Category.Values.Unit)]
        public class UpdateEndpointValidatorTests
        {
            [Theory, CreateDataAttributes]
            [Trait(TraitsConstants.Label.Name, TraitsConstants.Label.Values.Feature)]
            public void Validate_EmptySerialNumber_Throws(UpdateEndpointValidator subject)
            {
                // Arrange

                // Act
                var result = subject.TestValidate(new UpdateEndpoint { SwitchState = (SwitchStates)1, SerialNumber = string.Empty });

                // Assert
                result.ShouldHaveValidationErrorFor(c => c.SerialNumber);
            }

            [Theory]
            [InlineData("SerialNumber", -1)]
            [Trait(TraitsConstants.Label.Name, TraitsConstants.Label.Values.Feature)]
            public void Validate_InvalidSwitchStates_Throws(string serial, int state)
            {
                // Arrange
                var subject = new UpdateEndpointValidator();

                // Act
                var result = subject.TestValidate(new UpdateEndpoint { SwitchState = (SwitchStates)state, SerialNumber = serial });

                // Assert
                result.ShouldHaveValidationErrorFor(c => c.SwitchState);
            }

            [Theory, CreateDataAttributes]
            [Trait(TraitsConstants.Label.Name, TraitsConstants.Label.Values.Feature)]
            public void Validate_ValidCommand_Returns(UpdateEndpoint command, UpdateEndpointValidator subject)
            {
                // Arrange

                // Act
                var result = subject.TestValidate(command);

                // Assert
                result.ShouldNotHaveAnyValidationErrors();
            }
        }

        [Trait(TraitsConstants.Category.Name, TraitsConstants.Category.Values.Unit)]
        public class UpdateEndpointHandlerTests
        {
            [Theory, CreateDataAttributes]
            [Trait(TraitsConstants.Label.Name, TraitsConstants.Label.Values.Feature)]
            public async Task Handle_NullCommand_ThrowsArgumentNullException(
                UpdateEndpointHandler subject
                )
            {
                // Arrange

                // Act
                Func<Task> act = () => subject.Handle(null, default);

                // Assert
                await act.Should().ThrowExactlyAsync<ArgumentNullException>();
            }

            [Theory, CreateDataAttributes]
            [Trait(TraitsConstants.Label.Name, TraitsConstants.Label.Values.Feature)]
            public async Task Handle_EndpointNotFound_ThrowsKeyNotFoundException(
                UpdateEndpoint command,
                UpdateEndpointHandler subject
                )
            {
                // Arrange

                // Act
                Func<Task> act = () => subject.Handle(command, default);

                // Assert
                await act.Should().ThrowExactlyAsync<KeyNotFoundException>();
            }

            [Theory, CreateDataAttributes]
            [Trait(TraitsConstants.Label.Name, TraitsConstants.Label.Values.Feature)]
            public async Task Handle_EndpointFound_Returns(
                Endpoint endpoint,
                UpdateEndpoint command,
                [Frozen] LandisGyrContext context,
                UpdateEndpointHandler subject
                )
            {
                // Arrange
                context.Endpoints.Add(endpoint);
                await context.SaveChangesAsync();
                command.SerialNumber = endpoint.SerialNumber;
                command.SwitchState = endpoint.SwitchState;

                // Act
                var result = await subject.Handle(command, default);

                // Assert
                result.Should().NotBeNull();
            }
        }
    }
}
