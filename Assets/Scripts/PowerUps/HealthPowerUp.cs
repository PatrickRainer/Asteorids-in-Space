using Spaceships;
using UnityEngine;

namespace PowerUps
{
    public class HealthPowerUp : PowerUpBase
    {
        protected override void PowerUpAction()
        {
           // Debug.Log($"My power up action, by {gameObject.name}");

            GameManager.IncreaseLives();
        }
    }
}