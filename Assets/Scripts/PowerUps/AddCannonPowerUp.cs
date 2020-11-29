using Spaceships;
using UnityEngine;

namespace PowerUps
{
    public class AddCannonPowerUp : PowerUpBase
    {
        protected override void PowerUpAction()
        {
           var playerShip = GameObject.FindObjectOfType<Spaceship>();
           playerShip._activeBulletAnchors = Mathf.Clamp(playerShip._activeBulletAnchors + 1, 1, 3);
        }
    }
}