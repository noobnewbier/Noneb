using Main.Core.Game.GameEnvironments.Data;
using Main.Core.Game.GameEnvironments.Save;

namespace Main.Core.InGameEditor.Save
{
    public interface ISaveEnvironmentService
    {
        SavingResult TrySaveEnvironment(GameEnvironment gameEnvironment, string environmentName);
    }
}