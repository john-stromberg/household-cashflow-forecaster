# Household Cashflow Forecaster

Workflow-oriented advisor tool for quarterly/yearly liquidity forecasting with stress toggles.

## Stack
- .NET 10 ASP.NET Core Web API
- Lightweight web UI for scenario input

## MVP Features
- Baseline quarterly cashflow forecast
- Stress toggles:
  - Market drop impact on contributions
  - Inflation spike impact on spending
  - Unexpected expense event
- Year/quarter output with ending liquidity path

## Run
```bash
dotnet restore household-cashflow-forecaster.slnx
dotnet run --project src/HouseholdCashflowForecaster.Api/HouseholdCashflowForecaster.Api.csproj
```

Open:
- `/` for UI
- `POST /api/cashflow-forecast/run` for API
