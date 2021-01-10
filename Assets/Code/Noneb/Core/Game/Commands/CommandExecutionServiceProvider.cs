using System;
using Noneb.Core.Game.Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Core.Game.Commands
{
    [CreateAssetMenu(fileName = nameof(CommandExecutionServiceProvider), menuName = MenuName.Providers + nameof(CommandExecutionService))]
    public class CommandExecutionServiceProvider : ScriptableObject, IObjectProvider<ICommandExecutionService>
    {
        private readonly Lazy<ICommandExecutionService> _lazy = new Lazy<ICommandExecutionService>(() => new CommandExecutionService());
        
        public ICommandExecutionService Provide() => _lazy.Value;
    }
}