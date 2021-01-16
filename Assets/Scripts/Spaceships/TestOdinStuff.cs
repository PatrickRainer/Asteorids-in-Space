using Sirenix.OdinInspector;
using UnityEngine;

namespace Spaceships
{
    public class TestOdinStuff : MonoBehaviour
    {
        [FoldoutGroup("Foldout")]
        [HorizontalGroup("Foldout/one")]
        [BoxGroup("Foldout/one/throttling")]
        public int throttleSpeed = 5;

        [BoxGroup("Foldout/one/rotating")]
        public int rotationSpeed = 10;
    }
}