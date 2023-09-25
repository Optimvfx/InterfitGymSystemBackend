namespace CLL.ControllersLogic;

public class Salary
{
    public uint Basic { get; set; }
    
    public uint Vacation { get; set; }

    public uint RecyclingAward { get; set; }
    public uint MissedWorkPenalty { get; set; }

    public uint Total => Basic + Vacation + RecyclingAward - MissedWorkPenalty;
}