using System.Runtime.InteropServices.WindowsRuntime;
using GameManagers;
using Spaceships;
using UnityEngine;

namespace PowerUps
{
    public abstract class PowerUpBase : MonoBehaviour
    {
        int _lastTriggered;
       protected GameManager GameManager;

        void Start()
        {
            GameManager = FindObjectOfType<GameManager>();
        }


        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            //Debug.Log($"Power up {gameObject.name} is triggered");
            
            if (!other.CompareTag("Player")) return;

            if (_lastTriggered == other.gameObject.GetInstanceID()) return;
            _lastTriggered = other.gameObject.GetInstanceID();

           
            PowerUpAction();
            
            Destroy(gameObject,0.2f);
        }

       protected abstract void PowerUpAction();
    }
}