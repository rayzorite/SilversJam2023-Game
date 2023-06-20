using System;
using UnityEngine;

namespace Menu
{
    public class RandomMovement : MonoBehaviour
    {
        private void Update()
        {
            transform.position += new Vector3(0, Mathf.Sin(Time.time * 2f + transform.position.x * 0.01f) * 0.25f, 0);
        }
    }
}
