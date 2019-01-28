using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    StageDirector stageDirector;

    public GameObject player;
	
	void Start () {
        stageDirector = FindObjectOfType<StageDirector>();
	}
	
	
	void Update () {
		if(stageDirector.stageState == StageDirector.STAGESTATE.MOVE)
        {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
