using Sirenix.OdinInspector;
using Spaceships;
using UnityEngine;
using UnityEngine.Serialization;

namespace PowerUps
{
    class RocketPowerUp : PowerUpBase
    {
        [SerializeField, AssetsOnly, Required] GameObject rocketPrefab;

        protected override void PowerUpAction()
        {
            var playerShip = FindObjectOfType<Spaceship>();

            playerShip.AddRocketToLoad(rocketPrefab); //TODO: Shall we split Rockets and Missiles to a different load?
        }
    }
}