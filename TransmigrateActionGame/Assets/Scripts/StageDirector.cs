using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageDirector : MonoBehaviour {

    public GameObject gameover;

    public void DestroyStage(GameObject stage)
    {
        Destroy(stage);

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

}
