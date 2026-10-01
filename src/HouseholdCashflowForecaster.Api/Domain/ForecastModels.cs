namespace HouseholdCashflowForecaster.Api.Domain;

public sealed class CashflowForecastRequest
{
    public string HouseholdName { get; set; } = string.Empty;
    public decimal StartingLiquidity { get; set; }
    public decimal QuarterlyIncome { get; set; }
    public decimal QuarterlySpending { get; set; }
    public decimal ExpectedQuarterlyContribution { get; set; }
    public int Years { get; set; } = 3;

    public bool StressMarketDrop { get; set; }
    public bool StressInflationSpike { get; set; }
    public bool StressUnexpectedExpense { get; set; }
}

public sealed class QuarterForecast
{
    public int Year { get; set; }
    public int Quarter { get; set; }
    public decimal Income { get; set; }
    public decimal Spending { get; set; }
    public decimal Contributions { get; set; }
    public decimal NetFlow { get; set; }
    public decimal EndingLiquidity { get; set; }
}

public sealed class CashflowForecastResult
{
    public string HouseholdName { get; set; } = string.Empty;
    public decimal StartingLiquidity { get; set; }
    public decimal EndingLiquidity { get; set; }
    public decimal MinimumLiquidity { get; set; }
    public List<QuarterForecast> Quarters { get; set; } = [];
    public List<string> AppliedStresses { get; set; } = [];
}
