using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cutscene
{
    public class Cutscene : MonoBehaviour
    {
        IEnumerator Start()
        {
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene(3);
        }
    }
}
