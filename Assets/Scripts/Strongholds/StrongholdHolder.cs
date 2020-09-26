﻿using Common.BoardItems;
using Common.Holders;
using UnityEditor;
using UnityEngine;
using UnityUtils.Pooling;

namespace Strongholds
{
    public class StrongholdHolder : PooledMonoBehaviour, IBoardItemHolder<Stronghold>
    {
        public Stronghold Value { get; private set; }
        BoardItem IBoardItemHolder.Value => Value;

        public void Initialize(Stronghold stronghold)
        {
            Value = stronghold;
        }

        public Transform Transform => transform;

        private void OnDrawGizmosSelected()
        {
            if (Value == null)
            {
                return;
            }

            var style = new GUIStyle {normal = {textColor = Color.cyan}};
            Handles.Label(
                transform.position,
                Value.Data.Name,
                style
            );
        }
    }
}