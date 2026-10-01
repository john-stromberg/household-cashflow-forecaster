using HouseholdCashflowForecaster.Api.Application.Contracts;
using HouseholdCashflowForecaster.Api.Domain;

namespace HouseholdCashflowForecaster.Api.Application.Services;

public sealed class ForecastService : IForecastService
{
    public CashflowForecastResult BuildForecast(CashflowForecastRequest request)
    {
        var years = Math.Clamp(request.Years, 1, 10);

        var income = request.QuarterlyIncome;
        var spending = request.QuarterlySpending;
        var contributions = request.ExpectedQuarterlyContribution;

        var applied = new List<string>();

        if (request.StressInflationSpike)
        {
            spending *= 1.12m;
            applied.Add("Inflation Spike (+12% spending)");
        }

        if (request.StressMarketDrop)
        {
            contributions *= 0.70m;
            applied.Add("Market Drop (-30% contributions)");
        }

        var liquidity = request.StartingLiquidity;
        var minLiquidity = liquidity;
        var quarterResults = new List<QuarterForecast>();

        var totalQuarters = years * 4;
        for (var i = 1; i <= totalQuarters; i++)
        {
            var year = ((i - 1) / 4) + 1;
            var quarter = ((i - 1) % 4) + 1;

            var qIncome = income;
            var qSpending = spending;
            var qContrib = contributions;

            if (request.StressUnexpectedExpense && year == 1 && quarter == 2)
            {
                qSpending += 25000m;
                applied.Add("Unexpected Expense (+$25,000 in Y1 Q2)");
                request.StressUnexpectedExpense = false;
            }

            var netFlow = qIncome - qSpending + qContrib;
            liquidity += netFlow;
            minLiquidity = Math.Min(minLiquidity, liquidity);

            quarterResults.Add(new QuarterForecast
            {
                Year = year,
                Quarter = quarter,
                Income = qIncome,
                Spending = qSpending,
                Contributions = qContrib,
                NetFlow = netFlow,
                EndingLiquidity = liquidity
            });
        }

        return new CashflowForecastResult
        {
            HouseholdName = request.HouseholdName,
            StartingLiquidity = request.StartingLiquidity,
            EndingLiquidity = liquidity,
            MinimumLiquidity = minLiquidity,
            Quarters = quarterResults,
            AppliedStresses = applied.Distinct().ToList()
        };
    }
}
