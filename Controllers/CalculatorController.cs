using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace htmx_test.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculatorController : ControllerBase
{
    private readonly ILogger<CalculatorController> _logger;
    private static readonly Queue<FreelanceCalculatorResult> freelanceCalculatorResults = new Queue<FreelanceCalculatorResult>();
    public CalculatorController(ILogger<CalculatorController> logger)
    {
        _logger = logger;
    }

    [HttpGet("history")]
    public string GetHistory()
    {
        if (freelanceCalculatorResults.Count == 0)
            return "No history yet";

        var sb = new StringBuilder();
        sb.Append("<ul>");
        foreach (var result in freelanceCalculatorResults.AsEnumerable().Reverse())
        {
            sb.Append($"<li>Rate: {result.Rate} Vacation days: {result.VacationDays} Amount left: {result.Remaining}</li>");
        }
        sb.Append("</ul>");
        return sb.ToString();
    }

    [HttpGet(Name = "Calculator")]

    public string GetCalculator(int rate, int vacationDays)
    {
        var calculatorResult = new FreelanceCalculator().Calculate(rate, vacationDays);

        freelanceCalculatorResults.Enqueue(calculatorResult);
        if (freelanceCalculatorResults.Count > 10)
            freelanceCalculatorResults.Dequeue();

        var sb = new StringBuilder();
        sb.Append($"<p>Total salary cost: {calculatorResult.SalaryExpense}</p>");
        sb.Append($"<p>Pension cost {calculatorResult.Pension}</p>");
        sb.Append($"<p>Remaining: {calculatorResult.Remaining}</p>");

        return sb.ToString();
    }
}
