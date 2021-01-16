using Sirenix.OdinInspector;
using Spaceships;
using UnityEngine;

namespace PowerUps
{
    public class ClusterBombPowerUp : PowerUpBase
    {
        [SerializeField, AssetsOnly, Required] GameObject clusterBombPrefab;
        
        protected override void PowerUpAction()
        {
            var playerShip = FindObjectOfType<Spaceship>();
            
            playerShip.AddRocketToLoad(clusterBombPrefab);
        }
    }
}