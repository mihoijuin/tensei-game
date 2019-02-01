using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {

    float opacity;
    public float blinkSpeed;
    public float blinkOffset;
    public float minOpacity;

    SpriteRenderer itemRenderer;
    Color originColor;

    StageDirector stageDirector;
	
	void Start () {
        stageDirector = FindObjectOfType<StageDirector>();
        itemRenderer = GetComponent<SpriteRenderer>();
        originColor = itemRenderer.color;
    }
	
	
	void Update () {
        if(stageDirector.stageState == StageDirector.STAGESTATE.INSTAGE)
        {
            opacity = Mathf.PingPong(Time.time * blinkSpeed, blinkOffset) + minOpacity;
            itemRenderer.color = new Color(originColor.r, originColor.g, originColor.b, opacity);
        }
    }
}
