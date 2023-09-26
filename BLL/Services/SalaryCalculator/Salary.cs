using DAL.Entities.Gym.Person;

namespace BLL.Services.SalaryCalculator;

public class Salary
{
    public readonly Employee Employee;

    public readonly uint Basic;

    public readonly uint Vacation;

    public readonly uint RecyclingAward;
    public readonly uint MissedWorkPenalty;

    public uint Total => Basic + Vacation + RecyclingAward - MissedWorkPenalty;

    public Salary(Employee employee, uint basic, uint vacation, uint recyclingAward, uint missedWorkPenalty)
    {
        Employee = employee;
        Basic = basic;
        Vacation = vacation;
        RecyclingAward = recyclingAward;
        MissedWorkPenalty = missedWorkPenalty;
    }
}