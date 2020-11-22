using System.Collections;
using System.Collections.Generic;
using GameManagers;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = System.Random;

namespace CoreMechanics
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] [Title("Objects to Spawn")] [AssetsOnly]
        List<GameObject> spawnObjects = new List<GameObject>();

        [SerializeField] float spawnExtraRadius = 0.5f;
        readonly Random _random = new Random();

        [ShowInInspector] [ReadOnly] float _currentSpawnInterval;
        DifficultyController _difficultyController;

        GameManager _gameManager;

        void Start()
        {
            //Positioning at Top of Screen
            gameObject.transform.position = Positioner.GetScreenTopMiddle();

            _gameManager = FindObjectOfType<GameManager>();
            _currentSpawnInterval = FindObjectOfType<GameManager>().startSpawnInterval;
            _difficultyController = FindObjectOfType<DifficultyController>();
            StartCoroutine(nameof(SpawnRandom));

            _difficultyController.IncreaseDifficulty += IncreaseDifficulty;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(Vector3.zero, GetSpawnRadius());
        }

        void IncreaseDifficulty(float stepToIncrease, float minSpawnInterval)
        {
            //Debug.Log("Increase Difficulty");
            if (_currentSpawnInterval <= minSpawnInterval) return;

            _currentSpawnInterval -= stepToIncrease;
        }

        float GetSpawnRadius()
        {
            var spawnRadius = Positioner.GetDistanceToScreenEdge() + spawnExtraRadius;
            return spawnRadius;
        }

        IEnumerator SpawnRandom()
        {
            while (_currentSpawnInterval > 0)
            {
                //Random Object
                var rndObject = spawnObjects[_random.Next(0, spawnObjects.Count)];

                // Spawn around circle, outside of the screen
                Instantiate(rndObject, MathExtras.RandomCircle(Vector3.zero, GetSpawnRadius()), Quaternion.identity);

                yield return new WaitForSeconds(_currentSpawnInterval);
            }
        }
    }
}