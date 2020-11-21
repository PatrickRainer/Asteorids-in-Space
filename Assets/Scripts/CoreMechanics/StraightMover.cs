using UnityEngine;

namespace CoreMechanics
{
    public abstract class StraightMover : MonoBehaviour
    {
        [SerializeField] protected Vector2 velocity = new Vector2();
        Movement _movement;

        protected virtual void Start()
        {
            _movement = new Movement(GetComponent<Rigidbody2D>(), velocity);
        }

        protected virtual void FixedUpdate()
        {
            _movement.Move();
        }
    }
}