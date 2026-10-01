using HouseholdCashflowForecaster.Api.Domain;

namespace HouseholdCashflowForecaster.Api.Application.Contracts;

public interface IForecastService
{
    CashflowForecastResult BuildForecast(CashflowForecastRequest request);
}
