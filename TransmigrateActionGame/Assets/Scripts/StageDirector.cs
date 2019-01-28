using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDirector : MonoBehaviour {

    public GameObject gameover;

    public void DestroyStage(GameObject stage)
    {
        Destroy(stage);

    }

    public void ShowGameOver()
    {
        gameover.SetActive(true);
    }

}
