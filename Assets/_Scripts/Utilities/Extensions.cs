using UnityEngine;

namespace _Scripts.Utilities
{
    public static class Extensions
    {
        public static void LockRotation(this Transform transform, bool lockX, bool lockY, bool lockZ)
        {
            var currentRotation = transform.rotation.eulerAngles;

            if (lockX)
            {
                currentRotation.x = 0f;
            }
            if (lockY)
            {
                currentRotation.y = 0f;
            }
            if (lockZ)
            {
                currentRotation.z = 0f;
            }

            transform.rotation = Quaternion.Euler(currentRotation);
        }
    }
}
