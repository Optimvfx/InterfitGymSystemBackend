using BLL.Models.Visitation;
using Common.Models;

namespace BLL.Services.VisitationResultFactory;

public abstract class BaseVisitationFactory
{
    public VisitationVM GetVisitationVM()
    {
        var latestTime = GetUpdateTimeInMinutes();
        var passedTimeProcentForUpdate = GetPassedTimeProcentForUpdate();
        var optimalTime = latestTime * passedTimeProcentForUpdate;
        
        return new VisitationVM()
        {
            LatestContinumeTimeInMinutes = latestTime,
            OptimalContinumeTimeInMinutes = (uint)optimalTime
        };
    }

    public abstract uint GetUpdateTimeInMinutes();
    public abstract Procent GetPassedTimeProcentForUpdate();
}