using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossExplosion : MonoBehaviour
{
    private PlayerController playerController;
    private string sceneName;

    public void SetUp(PlayerController playerController, string sceneName)
    {
        this.playerController = playerController;
        this.sceneName = sceneName;
    }

    private void OnDestroy()
    {
        playerController.Score += 1000;
        PlayerPrefs.SetInt("Score", playerController.Score);

        SceneManager.LoadScene(sceneName);
    }
}
