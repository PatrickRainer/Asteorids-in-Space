using System;
using GameManagers;
using Sirenix.OdinInspector;
using Spaceships;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Ui
{
    public class UiController : MonoBehaviour
    {
       [SerializeField, Required] TextMeshProUGUI scoreText;
       [SerializeField, Required] TextMeshProUGUI livesText;
       [SerializeField, Required] GameObject gameOverPanel;
       [SerializeField, Required] Image throttleBar;
       [SerializeField, Required] TextMeshProUGUI rocketCounterText;
       [SerializeField, Required] TextMeshProUGUI missileCounterText;
       [SerializeField, Required] TextMeshProUGUI clusterBombCounterText;

       Spaceship _currentSpaceship;

       void Start()
       {
           _currentSpaceship = FindObjectOfType<Spaceship>();
       }

       void OnGUI()
       {
           scoreText.text = GameManager.CurrentScore.ToString();
           livesText.text = GameManager.GetLives().ToString();

           if (_currentSpaceship != null)
           {
               throttleBar.fillAmount = _currentSpaceship.GetCurrentThrottlePercentage();
               rocketCounterText.text = _currentSpaceship.GetRocketLoadCount().ToString();
               missileCounterText.text = _currentSpaceship.GetMissileLoadCount().ToString();
               clusterBombCounterText.text = _currentSpaceship.GetClusterBombLoadCount().ToString();
           }
           else
           {
               _currentSpaceship = FindObjectOfType<Spaceship>();
           }
       }

       public void ActivateGameOverPanel()
       {
           gameOverPanel.SetActive(true);
       }

       public void MissileButtonPressed()
       {
           Debug.Log("Missile button pressed!");
           //TODO: Implement Missile Button
       }
       
       public void RocketButtonPressed()
       {
           Debug.Log("Rocket button pressed!");
           //TODO: Implement Rocket Button
       }
       
       public void ClusterBombButtonPressed()
       {
           Debug.Log("Cluster bomb button pressed!");
           //TODO: Implement Cluster bomb button
       }
    }
}