using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeDirector : MonoBehaviour {

    float lifePoint;
    float maxLifePoint = 3f;
    public float damagePoint;

    public GameObject lifes;


    void Start () {
        InitLife();
	}
	
	
	void Update () {

        if(lifePoint < Mathf.Epsilon)
        {
            Debug.Log("Game Over");
        }
    }

    // ダメージ
    public void DamageLifePoint()
    {
        
        GameObject targetLife;
        lifePoint -= damagePoint;

        // ライフの状態によって減らす対象のハートオブジェクトを変える
        if (lifePoint >= 2f)
        {
            targetLife = lifes.transform.GetChild(0).gameObject;
            targetLife.GetComponent<Image>().fillAmount -= damagePoint;
        }else if(lifePoint >= 1f && lifePoint < 2f)
        {
            targetLife = lifes.transform.GetChild(1).gameObject;
            targetLife.GetComponent<Image>().fillAmount -= damagePoint;
        }
        else
        {
            targetLife = lifes.transform.GetChild(2).gameObject;
            targetLife.GetComponent<Image>().fillAmount -= damagePoint;
        }
    }


    // 初期化
    void InitLife()
    {
        lifePoint = maxLifePoint;
        // TODO 最大ライフによって出現ハート数が増えるとかも拡張としてはあり

    }


}
