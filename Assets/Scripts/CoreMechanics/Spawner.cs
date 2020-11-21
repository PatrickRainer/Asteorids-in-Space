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

        [ShowInInspector, ReadOnly] float _currentSpawnInterval;

        GameManager _gameManager;
        readonly Random _random = new Random();
        DifficultyController _difficultyController;

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

        void IncreaseDifficulty(float stepToIncrease, float minSpawnInterval)
        {
           //Debug.Log("Increase Difficulty");
           if (_currentSpawnInterval <= minSpawnInterval) return;

           _currentSpawnInterval -= stepToIncrease;
        }

        IEnumerator SpawnRandom()
        {
            while (_currentSpawnInterval > 0)
            {
                //Object
                var rndObject = spawnObjects[_random.Next(0, spawnObjects.Count)];

                //Main Camera
                var mainCamera = Camera.main;
                if (mainCamera is null) yield break;

                //Screen Positions
                var leftTopCorner =
                    mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height,
                        mainCamera.nearClipPlane));
                var rightTopCorner =
                    mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.nearClipPlane));
                var topOfScreen = leftTopCorner.y;

                //Start Position
                var randomPosition = _random.Next((int) leftTopCorner.x, (int) rightTopCorner.x);
                var startPos = new Vector2(randomPosition, topOfScreen);

                //Instantiate
                var iObj = Instantiate(rndObject, startPos, Quaternion.identity);

                yield return new WaitForSeconds(_currentSpawnInterval);
            }
        }
    }
}