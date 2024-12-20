using System.Net;
using GoldPrices.Features.GoldPrices.Commands;
using GoldPrices.Features.GoldPrices.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoldPrices.Features.GoldPrices.Controllers;

[ApiController]
public class GoldPricesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<GoldPricesController> _logger;

    public GoldPricesController(IMediator mediator, ILogger<GoldPricesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("/api/prices/{request}")]
    [ProducesResponseType<int>((int)HttpStatusCode.NotFound)]
    [ProducesResponseType<int>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetPrice(
        [FromQuery] GetGoldPrice request,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching price with ID of {ID}", request.Id);
        var result = await _mediator.Send(request, cancellationToken);

        if (result is null)
        {
            _logger.LogInformation("Price {ID} not found.", request.Id);
            return NotFound($"Record {request.Id} not found");
        }

        return Ok(result);
    }

    [HttpGet("/api/prices")]
    [ProducesResponseType<int>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetPrices(
        [FromQuery] GetGoldPrices request,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching prices.");
        var result = await _mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPost("/api/prices")]
    [ProducesResponseType<int>((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType<int>((int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreatePrice(
        [FromBody] CreateGoldPrice request,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating new price {Request}.", request);
        var result = await _mediator.Send(request, cancellationToken);

        return CreatedAtAction(nameof(GetPrice), result);
    }
}