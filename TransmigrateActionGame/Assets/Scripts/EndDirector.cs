using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndDirector : MonoBehaviour {

    public GameObject goodEnd;
    public GameObject normalEnd;
    public GameObject badEnd;

    // グッドエンド
    public GameObject pandaBaby;
    public GameObject kawaiiTop;
    public GameObject kawaiiBottom;
    public GameObject goodEndTitle;
    public GameObject restart;
    public Sprite goodend_title_red;
    public Sprite goodend_title_white;


    BGMDirector bgmDirector;

    public float intarval;
    public float goodendInterval;

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
        if (restart.activeSelf)
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

        StartCoroutine(GoodEndMotion());

        yield return new WaitForSeconds(intarval + 5f);

        // リスタート説明を表示
        restart.SetActive(true);
    }

    IEnumerator GoodEndMotion()
    {
        Image titleImage = goodEndTitle.GetComponent<Image>();

        while (true)
        {
            yield return new WaitForSeconds(goodendInterval);
            titleImage.sprite = goodend_title_white;
            pandaBaby.transform.Translate(0f, 10f, 0f);
            

            yield return new WaitForSeconds(goodendInterval);
            titleImage.sprite = goodend_title_red;
            pandaBaby.transform.Translate(0f, -10f, 0f);
            kawaiiBottom.SetActive(true);

            yield return new WaitForSeconds(goodendInterval);
            titleImage.sprite = goodend_title_white;
            pandaBaby.transform.Translate(0f, 10f, 0f);
            kawaiiBottom.SetActive(false);
            kawaiiTop.SetActive(true);

            yield return new WaitForSeconds(goodendInterval);
            titleImage.sprite = goodend_title_red;
            kawaiiTop.SetActive(false);
            pandaBaby.transform.Translate(0f, -10f, 0f);

        }

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
