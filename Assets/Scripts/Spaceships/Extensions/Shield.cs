using System;
using UnityEngine;

namespace Spaceships.Extensions
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] int hitPoints = 10;

        void OnTriggerEnter(Collider other)
        {
            Debug.Log($"Shield: {gameObject.name} is hit!");
            //TODO: Implement a shield mechanic
        }
    }
}
