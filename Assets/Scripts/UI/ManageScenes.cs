using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{
    public void ChangeScene(string newSceneName)
    {
        SceneManager.LoadSceneAsync(newSceneName, LoadSceneMode.Single);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
