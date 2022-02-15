using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using UserManagementService.API.Endpoints;
using UserManagementService.Core.UserAggregate;
using UserManagementService.Core.UserAggregate.DTOs;
using UserManagementService.Core.UserAggregate.Query;
using Xunit;
using UserManagementService.Infrastructure;

namespace UserManagementService.API.Test.Integration.Endpoints;

public class UserEndpointsTests
{
    private readonly IMediator _mediator = Substitute.For<IMediator>();
    private readonly UserEndpointDefinition _sut = new();

    [Fact]
    public async Task GetAllUsers_ReturnNoContent_WhenNullAsync()
    {
        //Arrange
        _mediator.Send(Arg.Any<GetAllUsersQuery>()).Returns((List<User>)null);

        await using var app = new TestApplicationFactory(x =>
        {
            x.AddSingleton(_mediator);
            x.AddCoreServices(new ConfigurationManager());
        });

        var httpClient = app.CreateClient();

        //Act
        var response = await httpClient.GetAsync("users");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task GetAllUsers_ReturnNoContent_WhenNoUsersExistAsync()
    {
        //Arrange
        _mediator.Send(Arg.Any<GetAllUsersQuery>()).Returns(new List<User>());

        await using var app = new TestApplicationFactory(x =>
        {
            x.AddSingleton(_mediator);
            x.AddCoreServices(new ConfigurationManager());
        });

        var httpClient = app.CreateClient();

        //Act
        var response = await httpClient.GetAsync("users");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task GetAllUsers_ReturnsUser_WhenUserExists()
    {
        //Arrange
        var user = new User(Guid.NewGuid(), "Test Test");
        _mediator.Send(Arg.Any<GetAllUsersQuery>()).Returns(new List<User> { user });

        await using var app = new TestApplicationFactory(x =>
        {
            x.AddSingleton(_mediator);
            x.AddCoreServices(new ConfigurationManager());
        });

        var httpClient = app.CreateClient();

        //Act
        var response = await httpClient.GetAsync("users");
        var responseText = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        var userResult = JsonSerializer.Deserialize<List<UserDto>>(responseText, options);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        userResult?.FirstOrDefault()?.Name.Should().BeEquivalentTo(user.Name);
    }

}
