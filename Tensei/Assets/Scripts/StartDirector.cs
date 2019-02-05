using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartDirector : MonoBehaviour {

    BGMDirector bgmDirector;

    private void Start()
    {
        bgmDirector = FindObjectOfType<BGMDirector>();
        bgmDirector.PlayStageMusic();
    }


    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
