using UnityEngine;

namespace CoreMechanics
{
    public class Movement
    {
        readonly Rigidbody2D _rb;
        readonly Vector2 _velocity;

        public Movement(Rigidbody2D rb, Vector2 velocity)
        {
            _rb = rb;
            _velocity = velocity;
        }

        public void Move()
        {
            _rb.MovePosition(_rb.position + _velocity * Time.fixedDeltaTime);
        }
    }
}