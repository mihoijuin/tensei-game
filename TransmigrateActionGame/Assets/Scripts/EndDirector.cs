using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndDirector : MonoBehaviour {

    public GameObject goodEnd;
    public GameObject normalEnd;
    public GameObject badEnd;

    BGMDirector bgmDirector;

    public float intarval;

    private void Awake()
    {
        bgmDirector = FindObjectOfType<BGMDirector>();
    }

    void Start () {
        switch (ItemDirector.PointState)
        {
            case ItemDirector.POINTSTATE.GOOD:
                StartCoroutine(ShowGoodEnd());
                break;
            case ItemDirector.POINTSTATE.NORMAL:
                StartCoroutine(ShowNormalEnd());
                break;
            case ItemDirector.POINTSTATE.BAD:
                StartCoroutine(ShowBadEnd());
                break;
            default:
                Debug.Log("error");
                break;
        }
    }

    private void Update()
    {
        // リスタート説明が表示されたら、画面タップでスタートシーンに戻れる
        if (goodEnd.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.activeSelf)
        {

            if (Input.GetMouseButtonDown(0))
            {
                GoToStartScene();
            }
        }
    }


    IEnumerator ShowGoodEnd()
    {
        goodEnd.SetActive(true);
        yield return new WaitForSeconds(intarval);


        bgmDirector.PlayGoodendMusic();
        goodEnd.transform.GetChild(0).gameObject.SetActive(false);
        goodEnd.transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(intarval);

        // リスタート説明を表示
        goodEnd.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    IEnumerator ShowNormalEnd()
    {
        normalEnd.SetActive(true);
        yield return new WaitForSeconds(intarval);

        normalEnd.transform.GetChild(0).gameObject.SetActive(false);
        normalEnd.transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(intarval);

        GoToStartScene();

        yield break;
    }

    IEnumerator ShowBadEnd()
    {
        badEnd.SetActive(true);
        yield return new WaitForSeconds(intarval);

        badEnd.transform.GetChild(0).gameObject.SetActive(false);
        badEnd.transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(intarval);

        GoToStartScene();

        yield break;
    }


    void GoToStartScene()
    {
        SceneManager.LoadScene("StartScene"); 
    }
}
