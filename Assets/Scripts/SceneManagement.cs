using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagement : MonoBehaviour//Bu script silinecek
{
    public static SceneManagement Instance;
    private void Awake()
    {
        Instance = this;   
    }
   

    public void GameOver()
    {
        StartCoroutine(AddDelay());
    }
    public void LoadNextNevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        int totalLevelCount = SceneManager.sceneCount;
        if (nextLevel <= totalLevelCount)
            SceneManager.LoadScene(nextLevel);
        else
            SceneManager.LoadScene(0);
    }

    IEnumerator AddDelay()
    {
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
