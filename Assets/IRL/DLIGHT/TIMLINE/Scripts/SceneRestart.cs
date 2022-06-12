using UnityEngine;
using UnityEngine.SceneManagement;

namespace _IRL.Dignidad_Timeline.Scripts
{
    public class SceneRestart : MonoBehaviour
    {
        void Start()
        {
            SceneManager.LoadScene(0);
        }
    }
}