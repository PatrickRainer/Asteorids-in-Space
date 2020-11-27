using UnityEngine;

namespace CoreMechanics
{
    public class MathExtras
    {
        public static Vector3 RandomCircle(Vector3 center, float radius)
        {
            // create random angle between 0 to 360 degrees
            var ang = Random.value * 360;
            Vector3 pos;

            pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
            pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
            pos.z = center.z;

            return pos;
        }
    }
}