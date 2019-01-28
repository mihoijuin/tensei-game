using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    StageDirector stageDirector;

    public GameObject player;

    Vector3 offset;
	
	void Start () {
        stageDirector = FindObjectOfType<StageDirector>();
        offset = transform.position - player.transform.position;
	}
	
	
	void Update () {
		if(stageDirector.stageState == StageDirector.STAGESTATE.MOVE)
        {
            // TODO 入り出しが汚いので何かしらの処理をする
            transform.position = player.transform.position + offset;
        }
    }
}
