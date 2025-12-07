using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Inventive.AdminAPI.Models.V1.Equipment;
using Inventive.Core.Enums;
using Inventive.Test.Infrastructure;

namespace Inventive.Test.Integration.V1;

public class EquipmentIntegrationTests : IntegrationTestBase<AdminAPI.Program>
{
    [Fact]
    public async Task AddEquipment_WithValidData_ReturnsCreatedEquipment()
    {
        // Arrange
        var request = new AddEquipmentRequestModel
        {
            Name = "Test Equipment",
            Description = "Test Description",
            Length = 100,
            Width = 50,
            Height = 75,
            Weight = 25
        };

        // Act
        var response = await Client.PostAsJsonAsync("/v1/equipment", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var equipment = await response.Content.ReadFromJsonAsync<AddEquipmentResponseModel>();
        equipment.Should().NotBeNull();
        equipment!.Id.Should().NotBeEmpty();
        equipment.Name.Should().Be("Test Equipment");
        equipment.Description.Should().Be("Test Description");
        equipment.Status.Should().Be(EquipmentStatus.Available);
        equipment.Length.Should().Be(100);
        equipment.Width.Should().Be(50);
        equipment.Height.Should().Be(75);
        equipment.Weight.Should().Be(25);
        equipment.CreatedAt.Should().NotBe(default);
    }

    [Fact]
    public async Task GetAllEquipment_WithNoData_ReturnsEmptyList()
    {
        // Act
        var response = await Client.GetAsync("/v1/equipment?page=1&pageSize=20");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<GetAllEquipmentResponseModel>();
        result.Should().NotBeNull();
        result!.Items.Should().BeEmpty();
        result.TotalCount.Should().Be(0);
        result.Page.Should().Be(1);
        result.PageSize.Should().Be(20);
        result.TotalPages.Should().Be(0);
        result.HasNextPage.Should().BeFalse();
        result.HasPreviousPage.Should().BeFalse();
    }

    [Fact]
    public async Task GetAllEquipment_WithData_ReturnsPaginatedResults()
    {
        // Arrange - Create 3 equipment items
        for (var i = 1; i <= 3; i++)
        {
            var request = new AddEquipmentRequestModel
            {
                Name = $"Equipment {i}",
                Description = $"Description {i}",
                Length = 100 + i,
                Width = 50 + i,
                Height = 75 + i,
                Weight = 25 + i
            };
            await Client.PostAsJsonAsync("/v1/equipment", request);
        }

        // Act
        var response = await Client.GetAsync("/v1/equipment?page=1&pageSize=2");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<GetAllEquipmentResponseModel>();
        result.Should().NotBeNull();
        result!.Items.Should().HaveCount(2);
        result.TotalCount.Should().Be(3);
        result.Page.Should().Be(1);
        result.PageSize.Should().Be(2);
        result.TotalPages.Should().Be(2);
        result.HasNextPage.Should().BeTrue();
        result.HasPreviousPage.Should().BeFalse();

        // Verify second page
        var page2Response = await Client.GetAsync("/v1/equipment?page=2&pageSize=2");
        var page2Result = await page2Response.Content.ReadFromJsonAsync<GetAllEquipmentResponseModel>();
        page2Result.Should().NotBeNull();
        page2Result!.Items.Should().HaveCount(1);
        page2Result.HasNextPage.Should().BeFalse();
        page2Result.HasPreviousPage.Should().BeTrue();
    }

    [Fact]
    public async Task GetEquipmentById_WhenExists_ReturnsEquipment()
    {
        // Arrange - Create equipment
        var createRequest = new AddEquipmentRequestModel
        {
            Name = "Test Equipment",
            Description = "Test Description",
            Length = 100,
            Width = 50,
            Height = 75,
            Weight = 25
        };
        var createResponse = await Client.PostAsJsonAsync("/v1/equipment", createRequest);
        var createdEquipment = await createResponse.Content.ReadFromJsonAsync<AddEquipmentResponseModel>();

        // Act
        var response = await Client.GetAsync($"/v1/equipment/{createdEquipment!.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var equipment = await response.Content.ReadFromJsonAsync<GetEquipmentByIdResponseModel>();
        equipment.Should().NotBeNull();
        equipment!.Id.Should().Be(createdEquipment.Id);
        equipment.Name.Should().Be("Test Equipment");
        equipment.Description.Should().Be("Test Description");
        equipment.Status.Should().Be(EquipmentStatus.Available);
        equipment.Length.Should().Be(100);
        equipment.Width.Should().Be(50);
        equipment.Height.Should().Be(75);
        equipment.Weight.Should().Be(25);
    }

    [Fact]
    public async Task GetEquipmentById_WhenNotExists_ReturnsNotFound()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();

        // Act
        var response = await Client.GetAsync($"/v1/equipment/{nonExistentId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain($"Equipment with ID {nonExistentId} not found");
    }

    [Fact]
    public async Task AddEquipment_WithEmptyName_ReturnsBadRequest()
    {
        // Arrange
        var request = new AddEquipmentRequestModel
        {
            Name = "",
            Description = "Valid Description",
            Length = 100,
            Width = 50,
            Height = 75,
            Weight = 25
        };

        // Act
        var response = await Client.PostAsJsonAsync("/v1/equipment", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AddEquipment_WithNameTooLong_ReturnsBadRequest()
    {
        // Arrange
        var request = new AddEquipmentRequestModel
        {
            Name = new string('A', 201), // 201 characters, max is 200
            Description = "Valid Description",
            Length = 100,
            Width = 50,
            Height = 75,
            Weight = 25
        };

        // Act
        var response = await Client.PostAsJsonAsync("/v1/equipment", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AddEquipment_WithDescriptionTooLong_ReturnsBadRequest()
    {
        // Arrange
        var request = new AddEquipmentRequestModel
        {
            Name = "Valid Name",
            Description = new string('B', 1001), // 1001 characters, max is 1000
            Length = 100,
            Width = 50,
            Height = 75,
            Weight = 25
        };

        // Act
        var response = await Client.PostAsJsonAsync("/v1/equipment", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AddEquipment_WithZeroLength_ReturnsBadRequest()
    {
        // Arrange
        var request = new AddEquipmentRequestModel
        {
            Name = "Valid Name",
            Description = "Valid Description",
            Length = 0,
            Width = 50,
            Height = 75,
            Weight = 25
        };

        // Act
        var response = await Client.PostAsJsonAsync("/v1/equipment", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AddEquipment_WithNegativeWidth_ReturnsBadRequest()
    {
        // Arrange
        var request = new AddEquipmentRequestModel
        {
            Name = "Valid Name",
            Description = "Valid Description",
            Length = 100,
            Width = -50,
            Height = 75,
            Weight = 25
        };

        // Act
        var response = await Client.PostAsJsonAsync("/v1/equipment", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AddEquipment_WithNegativeHeight_ReturnsBadRequest()
    {
        // Arrange
        var request = new AddEquipmentRequestModel
        {
            Name = "Valid Name",
            Description = "Valid Description",
            Length = 100,
            Width = 50,
            Height = -75,
            Weight = 25
        };

        // Act
        var response = await Client.PostAsJsonAsync("/v1/equipment", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AddEquipment_WithNegativeWeight_ReturnsBadRequest()
    {
        // Arrange
        var request = new AddEquipmentRequestModel
        {
            Name = "Valid Name",
            Description = "Valid Description",
            Length = 100,
            Width = 50,
            Height = 75,
            Weight = -25
        };

        // Act
        var response = await Client.PostAsJsonAsync("/v1/equipment", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
