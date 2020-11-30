using Spaceships;
using UnityEngine;

namespace PowerUps
{
    public class AddCannonPowerUp : PowerUpBase
    {
        protected override void PowerUpAction()
        {
           var playerShip = GameObject.FindObjectOfType<Spaceship>();
           playerShip.ActiveBulletAnchors = Mathf.Clamp(playerShip.ActiveBulletAnchors + 1, 1, 3);
        }
    }
}