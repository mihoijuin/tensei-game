using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDirector : MonoBehaviour {

    float goodPoint;
    float badPoint;

    public float upSpeed;


    public Image goodMeter;
    public Image badMeter;

	void Start () {
        // 初期化
        goodPoint = 0f;
        badPoint = 0f;
	}
	
	
	void Update () {
		
	}

    // カウントアップ
    public void CountUpGoodPoint()
    {
        goodPoint += upSpeed;
        goodMeter.fillAmount += upSpeed;    // goodPointをそのまま入れるのではなく差分を足していくことで後にアニメーションで書き直しやすい気がする
    }


    public void CountUpBadPoint()
    {
        badPoint += upSpeed;
        badMeter.fillAmount += upSpeed;
    }
}
