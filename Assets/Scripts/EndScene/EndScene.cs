using System;
using UnityEngine;

namespace EndScene
{
    public class EndScene : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }
    }
}
