using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // 移動用
    public float updownSpeed;
    public float slideSpeed;
    public float playerRadius = 0.8f;

    public float stageMoveSpeed;

    // 揺らぎ
    public float playerScaleOffset;
    public float playerMinScale;
    float scaleCount;
    public float fluctuationSpeed;


    // 色変化
    [SerializeField]
    private float colorChangeAmount;

    // 死ぬ時
    public float flashSpeed;
    public float flashtimes;

    // ゴール判定
    public float goalSpeed;


    ItemDirector itemDirector;
    StageDirector stageDirector;

    public Rigidbody2D playerRigid;
    Renderer playerRenderer;

	void Start () {
        playerRigid = GetComponent<Rigidbody2D>();
        itemDirector = FindObjectOfType<ItemDirector>();
        stageDirector = FindObjectOfType<StageDirector>();
        playerRenderer = GetComponent<Renderer>();        
	}
	
	
	void FixedUpdate () {
        // 基本動作
        switch (stageDirector.stageState)
        {
            case StageDirector.STAGESTATE.INSTAGE:
                // 徐々に右に動く
                playerRigid.velocity = Vector2.right * slideSpeed;
                // 炎のようにゆらゆら
                scaleCount = Mathf.PingPong(Time.time * fluctuationSpeed, playerScaleOffset) + playerMinScale;
                transform.localScale = new Vector3(scaleCount, scaleCount, 1);
                break;
            case StageDirector.STAGESTATE.MOVE:
                // 移動
                playerRigid.velocity = Vector2.right * stageMoveSpeed;
                // 炎のようにゆらゆら
                scaleCount = Mathf.PingPong(Time.time * fluctuationSpeed, playerScaleOffset) + playerMinScale;
                transform.localScale = new Vector3(scaleCount, scaleCount, 1);
                break;
            case StageDirector.STAGESTATE.NONE:
                playerRigid.velocity = Vector2.zero;
                break;
        }

        // 上下移動
        if (Input.GetMouseButton(0) && stageDirector.stageState == StageDirector.STAGESTATE.INSTAGE)
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
        Color currentColor = playerRenderer.material.color;

        switch (collision.tag)
        {
            case "GoodItem":
                itemDirector.CountUpPoint();
                itemDirector.SwitchState();
                // 色を変更
                playerRenderer.material.color = new Color(currentColor.r, currentColor.g + colorChangeAmount, currentColor.b + colorChangeAmount);
                Destroy(collision.gameObject);
                break;
            case "BadItem":
                itemDirector.CountDownPoint();
                itemDirector.SwitchState();
                // 色を変更
                playerRenderer.material.color = new Color(currentColor.r, currentColor.g - colorChangeAmount, currentColor.b - colorChangeAmount);
                Destroy(collision.gameObject);
                break;
            case "Switch":
                StartCoroutine(PushSwitch(collision.gameObject));
                break;
            case "Goal":
                StartCoroutine(EnterGoal(collision.gameObject));
                break;
            default:
                Debug.Log("unknown collider");
                break;
       }

    }

    public IEnumerator Die()
    {
        yield return new WaitForSeconds(1f);

        for (int i=0; i<flashtimes; i++)
        {
            yield return new WaitForSeconds(flashSpeed);
            GetComponent<SpriteRenderer>().enabled = false;
            
            yield return new WaitForSeconds(flashSpeed);
            GetComponent<SpriteRenderer>().enabled = true;
        }

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);

        yield break;
    }


    IEnumerator PushSwitch(GameObject goalSwitch)
    {

        stageDirector.stageState = StageDirector.STAGESTATE.NONE;

        float targetPosX;
        float targetPosY;

        yield return new WaitForSeconds(0.5f);

        while ((goalSwitch.transform.position - transform.position).magnitude > 0.05f)
        {

            targetPosX = Mathf.SmoothStep(transform.position.x, goalSwitch.transform.position.x, goalSpeed);
            targetPosY = Mathf.SmoothStep(transform.position.y, goalSwitch.transform.position.y, goalSpeed);

            transform.position = new Vector3(targetPosX, targetPosY, transform.position.z);

            yield return new WaitForSeconds(0.01f);
        }

        stageDirector.DestroyStage(goalSwitch.transform.parent.gameObject);

        yield return new WaitForSeconds(0.5f);

        stageDirector.stageState = StageDirector.STAGESTATE.MOVE;

        yield break;
    }

    IEnumerator EnterGoal(GameObject goal)
    {
        stageDirector.stageState = StageDirector.STAGESTATE.NONE;

        float targetPosX;
        float targetPosY;

        yield return new WaitForSeconds(0.5f);

        while ((goal.transform.position - transform.position).magnitude > 0.05f)
        {

            targetPosX = Mathf.SmoothStep(transform.position.x, goal.transform.position.x, goalSpeed);
            targetPosY = Mathf.SmoothStep(transform.position.y, goal.transform.position.y, goalSpeed);

            transform.position = new Vector3(targetPosX, targetPosY, transform.position.z);

            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.5f);
        stageDirector.EndGame();
    }
}
