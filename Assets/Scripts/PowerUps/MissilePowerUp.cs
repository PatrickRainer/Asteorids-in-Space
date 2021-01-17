using Sirenix.OdinInspector;
using Spaceships;
using UnityEngine;
using UnityEngine.Serialization;

namespace PowerUps
{
    public class MissilePowerUp : PowerUpBase
    {
        [SerializeField, AssetsOnly, Required] GameObject missilePrefab;
        
        protected override void PowerUpAction()
        {
            var playerShip = FindObjectOfType<Spaceship>();
            
            playerShip.AddRocketToLoad(missilePrefab);
        }
    }
}