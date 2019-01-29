using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndDirector : MonoBehaviour {

    public GameObject goodEnd;
    public GameObject normalEnd;
    public GameObject badEnd;

    public float intarval;

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

    IEnumerator ShowGoodEnd()
    {
        goodEnd.SetActive(true);
        yield return new WaitForSeconds(intarval);

        goodEnd.transform.GetChild(0).gameObject.SetActive(false);
        goodEnd.transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(intarval);

        GoToStartScene();

        yield break;
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

    //IEnumerator ShowNormal()
    //{
    //    normalEnd.SetActive(true);
    //    Text endText = normalEnd.transform.GetChild(0).GetComponent<Text>();
    //    string endString = endText.text;

    //    // 最初は空の表示
    //    endText.text = "";

    //    char[] charList = { };

    //    yield break;

    //}

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
