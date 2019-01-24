using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // 移動用
    public float updownSpeed;
    public float slideSpeed;
    private readonly float playerRadius = 0.8f;

    // 揺らぎ
    public float playerScaleOffset;
    public float playerMinScale;
    float scaleCount;
    public float fluctuationSpeed;


    public float goalSpeed;
    public bool isInStage;


    ItemDirector itemDirector;
    StageDirector stageDirector;

    Rigidbody2D playerRigid;

	void Start () {
        playerRigid = GetComponent<Rigidbody2D>();
        itemDirector = FindObjectOfType<ItemDirector>();
        stageDirector = FindObjectOfType<StageDirector>();

        // TODO 最終的にはゲーム部分に突入したらONにする
        isInStage = true;
	}
	
	
	void Update () {

        // 基本動作
        if (isInStage)
        {
            // 炎のようにゆらゆら
            scaleCount = Mathf.PingPong(Time.time * fluctuationSpeed, playerScaleOffset) + playerMinScale;
            transform.localScale = new Vector3(scaleCount, scaleCount, 1);

            // 徐々に右に動く
            playerRigid.velocity = Vector2.right * slideSpeed;
        }
        else
        {
            playerRigid.velocity = Vector2.zero;
        }

        // 上下移動
        if (Input.GetMouseButton(0) && isInStage)
        {
            Vector3 inputPos = Input.mousePosition;
            Vector3 screenCenterPos = Camera.main.ViewportToScreenPoint(new Vector2(0, 0.5f));

            if(inputPos.y > screenCenterPos.y)
            {
                MoveUp();
            }
            else
            {
                MoveDown();
            }
        }

        // 色の変化

    }

    private void MoveUp()
    {
        Vector2 newPos = playerRigid.position + Vector2.up * updownSpeed;
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // 画面外には出ないようにする
        newPos.y = Mathf.Clamp(newPos.y, min.y, max.y - playerRadius);

        playerRigid.MovePosition(newPos);
    }

    private void MoveDown()
    {
        Vector2 newPos = playerRigid.position + Vector2.down * updownSpeed;
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // 画面外には出ないようにする
        newPos.y = Mathf.Clamp(newPos.y, min.y + playerRadius, max.y);

        playerRigid.MovePosition(newPos);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "GoodItem":
                itemDirector.CountUpPoint();
                itemDirector.SwitchState();
                Destroy(collision.gameObject);
                break;
            case "BadItem":
                itemDirector.CountDownPoint();
                itemDirector.SwitchState();
                Destroy(collision.gameObject);
                break;
            case "Switch":
                StartCoroutine(Goal(collision.gameObject));
                break;
            default:
                Debug.Log("unknown collider");
                break;
       }

    }

    IEnumerator Goal(GameObject goalSwitch)
    {

        isInStage = false;

        float targetPosX;
        float targetPosY;

        yield return new WaitForSeconds(0.5f);

        while ((goalSwitch.transform.position - transform.position).magnitude > 0.01f)
        {

            targetPosX = Mathf.SmoothStep(transform.position.x, goalSwitch.transform.position.x, goalSpeed);
            targetPosY = Mathf.SmoothStep(transform.position.y, goalSwitch.transform.position.y, goalSpeed);

            transform.position = new Vector3(targetPosX, targetPosY, transform.position.z);

            yield return new WaitForSeconds(0.01f);
        }

        stageDirector.DestroyStage(goalSwitch.transform.parent.gameObject);

        yield break;
    }
}
