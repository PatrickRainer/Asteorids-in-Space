using System;
using GameManagers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ui
{
    public class UiController : MonoBehaviour
    {
       [SerializeField] TextMeshProUGUI scoreText;
       [SerializeField] TextMeshProUGUI livesText;
       [SerializeField] GameObject gameOverPanel;

       void OnGUI()
       {
           scoreText.text = GameManager.CurrentScore.ToString();
           livesText.text = GameManager.GetLives().ToString();
       }

       public void ActivateGameOverPanel()
       {
           gameOverPanel.SetActive(true);
       }
    }
}