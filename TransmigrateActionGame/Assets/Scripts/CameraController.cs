using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    StageDirector stageDirector;

    public GameObject player;

    float offsetX;
	
	void Start () {
        stageDirector = FindObjectOfType<StageDirector>();
        offsetX = transform.position.x - player.transform.position.x;
	}
	
	
	void Update () {
		if(stageDirector.stageState == StageDirector.STAGESTATE.MOVE)
        {
            // TODO 入り出しが汚いので何かしらの処理をする
            transform.position = new Vector3(player.transform.position.x + offsetX, transform.position.y, transform.position.z);
        }
    }
}
