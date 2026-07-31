using Assets._Scripts.EnteryPoints;
using Assets._Scripts.ObjectsScripts.Coins;
using Assets._Scripts.ObjectsScripts.Points.CheckPoint;
using Assets._Scripts.ObjectsScripts.Points.Finish;
using Assets.Scripts.Points;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets._Scripts.Installers
{
    public class LevelScope : LifetimeScope
    {
        [SerializeField] private LevelConfig _levelConfig;

        [SerializeField] private FinishPointView _finishPoint;
        [SerializeField] private Transform _checkPoints; 
        [SerializeField] private Transform _coins; 
        [SerializeField] private Transform _botsRoad; 

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(new GamePoints(_finishPoint, _checkPoints, _coins, _botsRoad));

            var coinDictionary = DictionaryCoinViewCreate(_coins);
            builder.RegisterInstance(coinDictionary);

            builder.RegisterEntryPoint<CoinController>().AsSelf();
            builder.RegisterEntryPoint<CoinDictinaryModel>().AsSelf();
            builder.RegisterEntryPoint<CheckPointsController>().AsSelf();
            builder.RegisterEntryPoint<CheckPointDictinaryModel>().AsSelf();
            builder.RegisterEntryPoint<FinishController>().AsSelf();
            builder.RegisterEntryPoint<FinishModel>().AsSelf();
            builder.RegisterEntryPoint<LevelEnteryPoint>();
            builder.RegisterInstance(_levelConfig);

            builder.RegisterEntryPoint<LastCheckPointController>().AsSelf();
            builder.RegisterEntryPoint<LastCheckPointModel>().AsSelf();
        }

        private Dictionary<string, CoinView> DictionaryCoinViewCreate(Transform objectParent)
        {
            var views = new Dictionary<string, CoinView>();
            var coinViews = objectParent.GetComponentsInChildren<CoinView>();

            foreach (var view in coinViews)
            {
                if (!string.IsNullOrEmpty(view.Id))
                {
                    views[view.Id] = view;
                }
                else
                {
                    Debug.LogWarning($"CoinView without ID found: {view.name}");
                }
            }

            return views;
        }
    }
}
