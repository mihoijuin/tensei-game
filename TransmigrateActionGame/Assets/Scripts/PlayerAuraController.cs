using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAuraController : MonoBehaviour {

    StageDirector stageDirector;

    // 揺らぎ
    public float playerScaleOffset=0.1f;
    public float playerMinScale= 0.9f;
    public float fluctuationSpeed= 0.15f;
    float scaleCount;

    void Start () {
        stageDirector = FindObjectOfType<StageDirector>();
	}
	
	// Update is called once per frame
	void Update () {

        if(stageDirector.stageState != StageDirector.STAGESTATE.NONE)
        {
            // 炎のようにゆらゆら
            scaleCount = Mathf.PingPong(Time.time * fluctuationSpeed, playerScaleOffset) + playerMinScale;
            transform.localScale = new Vector3(scaleCount, scaleCount, 1);
        }
    }
}
