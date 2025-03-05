using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroStart : MonoBehaviour
{
    [SerializeField] int sceneIndex;
    public void NextScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
