using CodeBase.Infrastructure.DependencyInjection;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.GamePlay.Hero;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.LevelStates
{
    public class LevelBootStrappState : IEnterableState, IService
    {
        private IGameFactory gameFactory;
        private HeroSpawnPoint heroSpawnPoint;

        public LevelBootStrappState(IGameFactory gameFactory, HeroSpawnPoint heroSpawnPoint)
        {
            this.gameFactory = gameFactory;
            this.heroSpawnPoint = heroSpawnPoint;
        }

        public void Enter()
        {
            Debug.Log("LEVEL: Init");

            gameFactory.CreateHero(heroSpawnPoint.transform.position, heroSpawnPoint.transform.rotation);
            gameFactory.CreateJoystick();
            gameFactory.CreateFollowCamera().SetTarget(gameFactory.HeroObject.transform);
        }
    }
}
