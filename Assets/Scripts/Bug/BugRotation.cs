using UnityEngine;

namespace Bug
{
    public class BugRotation : MonoBehaviour
    {
        private void Update()
        {
            transform.Rotate(0, 2, 0);
        }
    }
}
