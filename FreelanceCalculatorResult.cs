namespace htmx_test;
public record FreelanceCalculatorResult
{
    public FreelanceCalculatorResult(int rate, int vacationDays)
    {
        Rate = rate;
        VacationDays = vacationDays;
    }

    public int SalaryExpense { get; internal set; }
    public int Pension { get; internal set; }
    public int Remaining { get; internal set; }
    public int Rate { get; }
    public int VacationDays { get; }
}
