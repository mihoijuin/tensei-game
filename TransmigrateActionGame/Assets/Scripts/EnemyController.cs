using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public int enemyPattern;
    public float switchSpeed;

    Vector3 originScale;
    Vector3 upDirection;

    RaycastHit2D hit;

    PlayerController playerController;


    void Start ()
    {
        playerController = FindObjectOfType<PlayerController>();

        originScale = transform.localScale;
        upDirection = new Vector3(originScale.x, -originScale.y, originScale.z);

        switch (enemyPattern)
        {
            case 1:
                StartCoroutine(SwitchDirectionPattern1());
                break;
        }
    }

    private void Update()
    {
        // Update内でステージ状況監視する以外思いつかなかった...
        if (!playerController.isInStage) { StopAllCoroutines(); }
    }

    // TODO コルーチンでなくUpdate()内で動かせたら理想
    // TODO プレイヤーがゴールについたら動きを停止させたい
    public IEnumerator SwitchDirectionPattern1()
    {
        // 上→下→右→左
        while (true)
        {
            TurnUp();
            yield return new WaitForSeconds(switchSpeed);

            TurnDown();
            yield return new WaitForSeconds(switchSpeed);

            TurnRight();
            yield return new WaitForSeconds(switchSpeed);

            TurnLeft();
            yield return new WaitForSeconds(switchSpeed);

        }

    }

    void TurnUp()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.up);

        if (hit && hit.collider.CompareTag("Player"))
        {
            Debug.Log("game over");
        }
        transform.localScale = upDirection;
    }

    void TurnDown()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.down);

        if (hit && hit.collider.CompareTag("Player"))
        {
            Debug.Log("game over");
        }
        transform.localScale = originScale;
    }

    void TurnRight()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.right);

        if (hit && hit.collider.CompareTag("Player"))
        {
            Debug.Log("game over");
        }
        Debug.Log("右");
    }

    void TurnLeft()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.left);

        if (hit && hit.collider.CompareTag("Player"))
        {
            Debug.Log("game over");
        }

        Debug.Log("左");
    }

}
