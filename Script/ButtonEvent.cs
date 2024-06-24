using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{
   public void SceneLoader(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        GameManager.instance.isDie = false;
        GameManager.instance.Score = 0;
    }
}
