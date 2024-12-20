using FluentValidation.TestHelper;
using GoldPrices.Features.GoldPrices.Queries;
using GoldPrices.Features.GoldPrices.Validators;
using Xunit;

namespace GoldPrices.Api.Tests.UnitTests.GoldPrices.Validators;

public class GetGoldPricesValidatorTests
{
    private readonly GetGoldPricesValidator _validator;

    public GetGoldPricesValidatorTests()
    {
        _validator = new GetGoldPricesValidator();
    }
    
    [Fact]
    public void Should_HaveErrors_When_StartDate_IsLater_Than_EndDate()
    {
        // Arrange
        var request = new GetGoldPrices
        {
            StartDate = new DateTime(2022, 01, 01),
            EndDate = new DateTime(2021, 01, 01),
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
        var request = new GetGoldPrices
        {
            StartDate = new DateTime(2021, 04, 01),
            EndDate = new DateTime(2021, 05, 01),
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
        var request = new GetGoldPrices
        {
            StartDate = new DateTime(2021, 04, 01),
            EndDate = new DateTime(2021, 04, 01),
        };
        
        // Act
        var validationResult = _validator.TestValidate(request);
        
        // Assert
        validationResult
            .ShouldNotHaveValidationErrorFor(x => x.StartDate);
    }
}