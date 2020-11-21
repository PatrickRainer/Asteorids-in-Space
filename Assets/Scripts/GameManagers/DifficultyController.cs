using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace GameManagers
{
    public class DifficultyController : MonoBehaviour
    {
        GameManager _gameManager;

        public Action<float, float> IncreaseDifficulty = delegate(float difficultyStep, float minSpawnInterval) {  };

        void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
            FindObjectOfType<CoreMechanics.Spawner>();

            StartCoroutine(nameof(DifficultyTimer));
        }

        IEnumerator DifficultyTimer()
        {
            while (true)
            {
                IncreaseDifficulty.Invoke(_gameManager.stepDifficultyIsRaised, _gameManager.minSpawnInterval);
                yield return new WaitForSeconds(_gameManager.secondsUntilDifficultyIsIncreased);
            }
        }

    }
}