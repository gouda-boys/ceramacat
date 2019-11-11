using System.Collections;
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
        StartCoroutine(waitToLoad());
    }

    IEnumerator waitToLoad()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("house");
    }

}
