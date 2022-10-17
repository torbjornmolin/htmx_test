namespace htmx_test;

public class FreelanceCalculator
{
    private readonly int workingDaysPerYear = 250;
    private readonly int monthlySalary = 46200;
    private readonly double employerTaxFactor = 0.3142;
    private readonly double taxDeductablePensionFactor = 0.3;
    private readonly int monthlyExpenses = 3000;
    public FreelanceCalculatorResult Calculate(int rate, int vacationDays)
    {
        var result = new FreelanceCalculatorResult(rate, vacationDays);

        var workingDays = workingDaysPerYear - vacationDays;
        var totalBilledHours = workingDays * 8;
        var totalBilledAmount = totalBilledHours * rate;

        var salaryExpense = monthlySalary * 12;
        var totalSalaryExpense = salaryExpense + salaryExpense * employerTaxFactor;

        result.SalaryExpense = Convert.ToInt32(Math.Round(totalSalaryExpense, 0));

        var pension = salaryExpense * taxDeductablePensionFactor;

        result.Pension = Convert.ToInt32(Math.Round(pension, 0));

        var remaining = totalBilledAmount - totalSalaryExpense - pension - monthlyExpenses * 12;
        result.Remaining = Convert.ToInt32(Math.Round(remaining, 0));
        return result;
    }
}