using GameEnvironments.Common;
using GameEnvironments.Common.Data;

namespace GameEnvironments.Save
{
    public interface ISaveEnvironmentService
    {
        SavingResult TrySaveEnvironment(GameEnvironment gameEnvironment, string environmentName);
    }
}