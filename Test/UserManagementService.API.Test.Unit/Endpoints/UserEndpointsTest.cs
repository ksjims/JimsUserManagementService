using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using UserManagementService.API.Endpoints;
using UserManagementService.API.Test.Unit.Extensions;
using UserManagementService.Core.UserAggregate;
using UserManagementService.Core.UserAggregate.Query;
using Xunit;

namespace UserManagementService.API.Test.Unit.Endpoints;

public class CustomerEndpointDefinitionTests
{
    private readonly IMediator _mediator = Substitute.For<IMediator>();
    private readonly UserEndpointDefinition _sut = new();

    [Fact]
    public async Task GetAllUsers_ReturnNoContent_WhenNullAsync()
    {
        //Arrange
        _mediator.Send(Arg.Any<GetAllUsersQuery>()).Returns((List<User>)null);

        //Act
        IResult result = await _sut.GetAllUsersAsync(_mediator);

        //Assert
        result.GetNoContentResultStatusCode().Should().Be(204);
    }

    [Fact]
    public async Task GetAllUsers_ReturnNoContent_WhenNoUsersExistAsync()
    {
        //Arrange
        _mediator.Send(Arg.Any<GetAllUsersQuery>()).Returns(new List<User>());

        //Act
        IResult result = await _sut.GetAllUsersAsync(_mediator);

        //Assert
        result.GetNoContentResultStatusCode().Should().Be(204);
    }

    [Fact]
    public async void GetAllUsers_ReturnsUser_WhenUserExists()
    {
        //Arrange
        var user = new User(Guid.NewGuid(), "Test Test");
        _mediator.Send(Arg.Any<GetAllUsersQuery>()).Returns(new List<User> { user });

        //Act
        IResult result = await _sut.GetAllUsersAsync(_mediator);
        
        //Assert
        result.GetOkObjectResultValue<List<UserDto>>().Should().ContainSingle(x => x.Id == user.Id && x.Name == user.Name);
    }
}

