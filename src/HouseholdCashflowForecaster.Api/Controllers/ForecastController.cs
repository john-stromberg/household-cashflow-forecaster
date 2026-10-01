using HouseholdCashflowForecaster.Api.Application.Contracts;
using HouseholdCashflowForecaster.Api.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HouseholdCashflowForecaster.Api.Controllers;

[ApiController]
[Route("api/cashflow-forecast")]
public sealed class ForecastController : ControllerBase
{
    private readonly IForecastService _forecastService;

    public ForecastController(IForecastService forecastService)
    {
        _forecastService = forecastService;
    }

    [HttpPost("run")]
    public ActionResult<CashflowForecastResult> Run([FromBody] CashflowForecastRequest request)
    {
        return Ok(_forecastService.BuildForecast(request));
    }
}
