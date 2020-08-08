using GameEnvironments.Common;
using GameEnvironments.Common.Data;
using GameEnvironments.Common.Data.GameEnvironments;

namespace GameEnvironments.Save
{
    public interface ISaveEnvironmentService
    {
        SavingResult TrySaveEnvironment(GameEnvironment gameEnvironment, string environmentName);
    }
}