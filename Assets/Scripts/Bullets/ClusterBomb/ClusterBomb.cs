using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Bullets
{
    public class ClusterBomb : WeaponBase
    {
        [SerializeField] [Required] [AssetsOnly]
        List<GameObject> clusters = new List<GameObject>();

        Vector2 _startPosition;
        Vector2 _endPosition; //Should move until this position and then stop
        float percentOfWay = 0;

        protected override void Awake()
        {
            // Set Start and Target position
            var position = transform.position;
            _startPosition = position;
            _endPosition = position + new Vector3(0, 2);

            base.Awake();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            //TODO: Shoot the single clusters in all directions
        }

        protected override void Move()
        {
            // percentOfWay += Time.fixedDeltaTime;
            // transform.position = Vector2.Lerp(_startPosition, _endPosition, percentOfWay);
            _rb.DOMove(_endPosition, 2);
        }
    }
}