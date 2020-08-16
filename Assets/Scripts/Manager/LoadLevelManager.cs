using System;
using GameEnvironments.Load.BoardItemOnTile.Loaders;
using GameEnvironments.Load.BoardItemOnTile.StrongholdInternalPosition;
using GameEnvironments.Load.GameObjects.Loaders;
using GameEnvironments.Load.Tiles;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace Manager
{
    public class LoadLevelManager : MonoBehaviour
    {
        [SerializeField] private MapLoader mapLoader;
        [SerializeField] private UnitLoader unitLoader;
        [SerializeField] private ConstructLoader constructLoader;
        [SerializeField] private StrongholdLoader strongholdLoader;

        [SerializeField] private TileGameObjectLoader tileGameObjectLoader;
        [SerializeField] private UnitGameObjectLoader unitGameObjectLoader;
        [SerializeField] private ConstructGameObjectLoader constructGameObjectLoader;
        [SerializeField] private StrongholdUnitGameObjectLoader strongholdUnitGameObjectLoader;
        [SerializeField] private StrongholdConstructGameObjectLoader strongholdConstructGameObjectLoader;
        [SerializeField] private StrongholdGameObjectsInternalPositionLoader strongholdGameObjectsInternalPositionLoader;

        [ContextMenu(nameof(Load))]
        public void Load()
        {
            mapLoader.LoadObservable()
                .Concat(LoadBoardItemOnTileHolders())
                .Concat(LoadGameObjects())
                .Subscribe();
        }

        private IObservable<Unit> LoadBoardItemOnTileHolders()
        {
            return unitLoader.LoadObservable()
                .Zip(constructLoader.LoadObservable(), 
                    strongholdLoader.LoadObservable(),
                    delegate { return Unit.Default; }
                );
        }
        
        private IObservable<Unit> LoadGameObjects()
        {
            return tileGameObjectLoader.LoadObservable()
                .Zip(unitGameObjectLoader.LoadObservable(), 
                    constructGameObjectLoader.LoadObservable(),
                    strongholdUnitGameObjectLoader.LoadObservable(),
                    strongholdConstructGameObjectLoader.LoadObservable(),
                    strongholdGameObjectsInternalPositionLoader.LoadObservable(),
                    delegate { return Unit.Default; }
                );
        }
    }
}