using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string newScene;
    public void GoToScene()
    {
        SceneManager.LoadScene(newScene);
    }
}
