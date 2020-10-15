using Noneb.Core.Game.GameEnvironments.Data;
using Noneb.Core.Game.GameEnvironments.Save;

namespace Noneb.Core.InGameEditor.Save
{
    public interface ISaveEnvironmentService
    {
        SavingResult TrySaveEnvironment(GameEnvironment gameEnvironment, string environmentName);
    }
}