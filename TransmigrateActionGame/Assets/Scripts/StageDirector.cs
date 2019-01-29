using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageDirector : MonoBehaviour {

    public GameObject gameover;
    public GameObject stage2;

    public GameObject player;

    public GameObject stage2Checker;

    public enum STAGESTATE
    {
        NONE = -1,

        INSTAGE = 0,
        MOVE
    }

    public STAGESTATE stageState;

    private void Start()
    {
        stageState = STAGESTATE.INSTAGE;
    }
    private void FixedUpdate()
    {
        if(player && player.transform.position.x > stage2Checker.transform.position.x && stageState == STAGESTATE.MOVE)
        {
            StartCoroutine(StartStage2());
        }
    }


    public void DestroyStage(GameObject stage)
    {
        Destroy(stage);
    }

    IEnumerator StartStage2()
    {
        stageState = STAGESTATE.INSTAGE;
        yield return new WaitForSeconds(1f);
        stage2.SetActive(true);
        yield break;
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);

        // ゲームオーバーを表示
        gameover.SetActive(true);

        yield return new WaitForSeconds(2f);

        Debug.Log("game over");

        // 少し待ってスタート画面に遷移
        SceneManager.LoadScene("StartScene");
    }

    public void EndGame()
    {
        SceneManager.LoadScene("EndScene");
    }
}
