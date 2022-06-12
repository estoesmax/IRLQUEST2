using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    [Button]
    public void Restart()
    {
        SceneManager.LoadScene("99_Restart");
    }
   
}
