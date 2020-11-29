using System;
using Sirenix.OdinInspector;
using Spaceships;
using UnityEngine;

namespace PowerUps
{
    public class ShieldPowerUp : PowerUpBase
    {
        [SerializeField, AssetsOnly, Required] GameObject shieldPrefab;
        protected override void PowerUpAction()
        {
            var spaceShip = FindObjectOfType<Spaceship>();

            Instantiate(shieldPrefab, spaceShip.transform);
        }
    }
}