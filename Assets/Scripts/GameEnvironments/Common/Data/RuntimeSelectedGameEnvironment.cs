using UnityEngine;
using UnityUtils.Constants;
using UnityUtils.ScriptableReference;

namespace GameEnvironments.Common.Data
{
    [CreateAssetMenu(fileName = nameof(RuntimeSelectedGameEnvironment), menuName = MenuName.RuntimeReference + nameof(GameEnvironment))]
    public class RuntimeSelectedGameEnvironment : RuntimeReference<GameEnvironment>
    {
    }
}