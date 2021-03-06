using System.Collections.Generic;
using GameManagers;
using UnityEngine;
using Random = System.Random;

namespace Asteroids


{
    /// <summary>
    /// Use the init method to initialize
    /// </summary>
    internal class PowerUpDropper : MonoBehaviour
    {
        readonly Random _random = new Random();
        Asteroid _asteroid;

        public void Init(Asteroid asteroid)
        {
            _asteroid = asteroid;
        }

        public void DropRandomPowerUp(Asteroid asteroid)
        {
            // Debug.Log("Power up dropper reacts to android destroy");
            var dropAtAll = _random.Next(0, 100) <= asteroid.chanceToDropPowerUp;

            if (dropAtAll)
            {
                var randomPowerUpIndex = _random.Next(0, asteroid.powerUps.Count);
                Instantiate(asteroid.powerUps[randomPowerUpIndex], transform.position, Quaternion.identity);
            }
        }
    }
}