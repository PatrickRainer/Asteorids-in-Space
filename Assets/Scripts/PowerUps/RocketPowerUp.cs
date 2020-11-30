using Sirenix.OdinInspector;
using Spaceships;
using UnityEngine;

namespace PowerUps
{
    public class RocketPowerUp : PowerUpBase
    {
        [SerializeField, AssetsOnly, Required] GameObject rocketPrefab;
        
        protected override void PowerUpAction()
        {
            var playerShip = FindObjectOfType<Spaceship>();
            
            playerShip.AddRocketToLoad(rocketPrefab);
        }
    }
}