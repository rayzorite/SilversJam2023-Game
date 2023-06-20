using UnityEngine;

namespace Menu
{
    public class CamRotation : MonoBehaviour
    {
        public float rotateSpeed;
        private void Update()
        {
            transform.RotateAround(Vector3.zero, Vector3.up, rotateSpeed * Time.deltaTime);
        }
    }
}
