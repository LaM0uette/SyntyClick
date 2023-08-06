using UnityEngine;

namespace Bug
{
    public class BugRotation : MonoBehaviour
    {
        private void FixedUpdate()
        {
            transform.Rotate(0, 2, 0);
        }
    }
}
