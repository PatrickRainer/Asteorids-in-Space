using UnityEngine;

namespace GameManagers
{
    public class TutorialController : MonoBehaviour
    {
        void Update()
        {
            // As soon a key is pressed disable the tutorial
            if (Input.anyKeyDown)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
