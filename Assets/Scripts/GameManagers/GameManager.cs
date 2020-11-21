using System;
using Sirenix.OdinInspector;
using Ui;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace GameManagers
{
    [Serializable]
    public class GameManager : MonoBehaviour
    {
        [Title("Difficulty Settings")] 
        public float secondsUntilDifficultyIsIncreased = 1f;
        public float stepDifficultyIsRaised = .1f;
        [SerializeField] int startLive = 3;
        public float startSpawnInterval = 3;
        public float minSpawnInterval = 0.5f;

        [Title("Current Status")] [ShowInInspector, ReadOnly]
        public static int CurrentLives = 3;

        [ShowInInspector, ReadOnly] public static int CurrentScore = 0;

        [Title("Other Settings")] [SerializeField, AssetsOnly, Required]
        GameObject spaceShip;

        public static bool IsSceneUnloading = false;


        void Start()
        {
            IsSceneUnloading = false;
            Time.timeScale = 1;
            CurrentScore = 0;
            CurrentLives = startLive;
        }


        void OnApplicationQuit()
        {
            var gos = FindObjectsOfType<GameObject>();
            foreach (var go in gos)
            {
                DestroyImmediate(go);
            }
        }

        public void DecreaseLives()
        {
            CurrentLives -= 1;
            if (CurrentLives <= 0)
            {
                //Time.timeScale = 0;
                FindObjectOfType<UiController>().ActivateGameOverPanel();
            }
        }

        public static int GetLives()
        {
            return CurrentLives;
        }

        public void RestartGame()
        {
            IsSceneUnloading = true;
            SceneManager.LoadScene(sceneBuildIndex: 0); //TODO: Load by Name
        }
    }
}