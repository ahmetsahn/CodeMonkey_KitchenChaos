using _Scripts.Utilities;
using UnityEngine;

namespace _Scripts
{
    public class LockRotation : MonoBehaviour
    {
        private void Update()
        {
            transform.LockRotation(true,true,true);
        }
    }
}