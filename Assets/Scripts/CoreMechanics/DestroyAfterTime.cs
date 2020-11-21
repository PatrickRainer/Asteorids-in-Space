using System;
using UnityEngine;

namespace CoreMechanics
{
    public class DestroyAfterTime : MonoBehaviour
    {
        [SerializeField] float timeUntilDestroy;

        void Awake()
        {
            Destroy(gameObject, timeUntilDestroy);
        }
    }
}
