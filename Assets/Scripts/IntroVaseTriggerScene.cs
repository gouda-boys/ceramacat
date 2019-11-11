using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroVaseTriggerScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("house");
    }
}
