using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour {

    GameObject player;
    StageDirector stageDirector;

	void Start () {
        player = GameObject.Find("Player");
        stageDirector = FindObjectOfType<StageDirector>();
	}
	
	void Update () {
	    	
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.transform.position = transform.position;
            stageDirector.DestroyStage(transform.parent.gameObject);
        }
    }
}
