using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // 移動用
    public float playerSpeed;
    private readonly float playerRadius = 1f;

    // 揺らぎ
    public float playerScaleOffset;
    public float playerMinScale;
    float scaleCount;
    public float fluctuationSpeed;


    Rigidbody2D playerRigid;
	void Start () {
        playerRigid = GetComponent<Rigidbody2D>();
	}
	
	
	void Update () {
        // 炎ぽい動きをつける
        scaleCount = Mathf.PingPong(Time.time * fluctuationSpeed, playerScaleOffset) + playerMinScale;
        transform.localScale = new Vector3(scaleCount, scaleCount, 1);

        // 移動
        if (Input.GetMouseButton(0))
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
        Vector2 newPos = playerRigid.position + Vector2.up * playerSpeed;
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // 画面外には出ないようにする
        newPos.y = Mathf.Clamp(newPos.y, min.y, max.y - playerRadius);

        playerRigid.MovePosition(newPos);
    }

    private void MoveDown()
    {
        Vector2 newPos = playerRigid.position + Vector2.down * playerSpeed;
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // 画面外には出ないようにする
        newPos.y = Mathf.Clamp(newPos.y, min.y + playerRadius, max.y);

        playerRigid.MovePosition(newPos);
    }
}
