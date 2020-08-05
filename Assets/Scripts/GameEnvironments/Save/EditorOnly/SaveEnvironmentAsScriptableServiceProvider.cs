using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Save.EditorOnly
{
    [CreateAssetMenu(
        fileName = nameof(SaveEnvironmentAsScriptableServiceProvider),
        menuName = MenuName.ScriptableService + nameof(SaveEnvironmentAsScriptableService)
    )]
    public class SaveEnvironmentAsScriptableServiceProvider : ScriptableObjectProvider<SaveEnvironmentAsScriptableService>
    {
        public override SaveEnvironmentAsScriptableService Provide()
        {
            return new SaveEnvironmentAsScriptableService();
        }
    }
}