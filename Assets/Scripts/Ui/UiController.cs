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
    }
}