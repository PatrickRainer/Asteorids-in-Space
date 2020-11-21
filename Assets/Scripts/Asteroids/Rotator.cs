using UnityEngine;

namespace Asteroids
{
    internal class Rotator
    {
        readonly int _rotationSpeed;
        readonly Rigidbody2D _rb;

        public Rotator(Rigidbody2D rb, int rotationSpeed = 100)
        {
            _rb = rb;
            _rotationSpeed = rotationSpeed;
        }

        public void Rotate()
        {
            _rb.MoveRotation(_rb.rotation + _rotationSpeed * Time.fixedDeltaTime);
        }
    }
}