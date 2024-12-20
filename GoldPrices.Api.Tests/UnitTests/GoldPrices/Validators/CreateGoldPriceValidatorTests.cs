using FluentValidation.TestHelper;
using GoldPrices.Features.GoldPrices.Commands;
using GoldPrices.Features.GoldPrices.Validators;
using Xunit;

namespace GoldPrices.Api.Tests.UnitTests.GoldPrices.Validators;

public class CreateGoldPriceValidatorTests
{
    private readonly CreateGoldPriceValidator _validator;

    public CreateGoldPriceValidatorTests()
    {
        _validator = new CreateGoldPriceValidator();
    }

    [Fact]
    public void Should_HaveErrors_When_StartDate_IsLater_Than_EndDate()
    {
        // Arrange
        var request = new CreateGoldPrice
        {
            StartDate = new DateTime(2022, 01, 01),
            EndDate = new DateTime(2021, 01, 01),
            AveragePrice = 10,
        };
        
        // Act
        var validationResult = _validator.TestValidate(request);
        
        // Assert
        validationResult
            .ShouldHaveValidationErrorFor(x => x.StartDate)
            .WithErrorMessage(CustomErrorMessages.StartDateEarlierThanEndDateMessage);
    }

    [Fact]
    public void Should_NotHaveErrors_When_StartDate_IsEarlier_Than_EndDate()
    {
        // Arrange
        var request = new CreateGoldPrice
        {
            StartDate = new DateTime(2021, 04, 01),
            EndDate = new DateTime(2021, 05, 01),
            AveragePrice = 10,
        };
        
        // Act
        var validationResult = _validator.TestValidate(request);
        
        // Assert
        validationResult
            .ShouldNotHaveValidationErrorFor(x => x.StartDate);
    }
    
    [Fact]
    public void Should_NotHaveErrors_When_StartDate_IsTheSame_As_EndDate()
    {
        // Arrange
        var request = new CreateGoldPrice
        {
            StartDate = new DateTime(2021, 04, 01),
            EndDate = new DateTime(2021, 04, 01),
            AveragePrice = 10,
        };
        
        // Act
        var validationResult = _validator.TestValidate(request);
        
        // Assert
        validationResult
            .ShouldNotHaveValidationErrorFor(x => x.StartDate);
    }
}