using Spaceships;
using UnityEngine;

namespace PowerUps
{
    public class AddCannonPowerUp : PowerUpBase
    {
        protected override void PowerUpAction()
        {
           var playerShip = GameObject.FindObjectOfType<Spaceship>();
           playerShip.ActiveCannons = Mathf.Clamp(playerShip.ActiveCannons + 1, 1, 3);
        }
    }
}