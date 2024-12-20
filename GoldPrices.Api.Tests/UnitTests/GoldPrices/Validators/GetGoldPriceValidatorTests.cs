using FluentValidation.TestHelper;
using GoldPrices.Features.GoldPrices.Queries;
using GoldPrices.Features.GoldPrices.Validators;
using Xunit;

namespace GoldPrices.Api.Tests.UnitTests.GoldPrices.Validators;

public class GetGoldPriceValidatorTests
{
    private readonly GetGoldPriceValidator _validator;

    public GetGoldPriceValidatorTests()
    {
        _validator = new GetGoldPriceValidator();
    }
    
    [Fact]
    public void Should_HaveErrors_When_Id_Is_Empty()
    {
        // Arrange
        var request = new GetGoldPrice
        {
            Id = Guid.Empty
        };
        
        // Act
        var validationResult = _validator.TestValidate(request);
        
        // Assert
        validationResult
            .ShouldHaveValidationErrorFor(x => x.Id);
    }
}