using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    StageDirector stageDirector;

    public GameObject player;

    float offsetX;
    public float followSpeed;
	
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

    public IEnumerator FollowPlayer()
    {
        Vector3 targetPos = new Vector3(player.transform.position.x + offsetX, transform.position.y, transform.position.z);
        float targetPosX;
        float targetPosY;

        while ((targetPos - transform.position).magnitude > 0.05f)
        {

            targetPosX = Mathf.SmoothStep(transform.position.x, targetPos.x, followSpeed);
            targetPosY = Mathf.SmoothStep(transform.position.y, targetPos.y, followSpeed);

            transform.position = new Vector3(targetPosX, targetPosY, transform.position.z);

            yield return new WaitForSeconds(0.001f);
        }

        stageDirector.stageState = StageDirector.STAGESTATE.MOVE;

        yield break;
    }
}
