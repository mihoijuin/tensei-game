using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public int enemyPattern;
    public float switchSpeed;


    Animator enemyAnimator;
    RaycastHit2D hit;

    PlayerController playerController;
    GameObject player;


    // TODO 最終的にはStartではなく、ステージアクティブになったら以下の処理を行う
    void Start ()
    {
        playerController = FindObjectOfType<PlayerController>();
        player = GameObject.Find("Player");

        enemyAnimator = GetComponent<Animator>();

        switch (enemyPattern)
        {
            case 1:
                StartCoroutine(SwitchDirectionPattern1());
                break;
        }
    }

    private void Update()
    {

        // ゲームステージ中のみ敵の動きを実行
        // Update内でステージ状況監視する以外思いつかなかった...
        if (!playerController.isInStage) { StopAllCoroutines(); }


        // 敵に見つかったらゲームオーバー
        if (hit && hit.collider.CompareTag("Player") && playerController.isInStage)
        {
            Attack();
        }

        // 敵にぶつかってもゲームオーバー
        if(playerController.isInStage && (player.transform.position - transform.position).magnitude < playerController.playerRadius * 1.5f)
        {
            Attack();
        }
    }

    // TODO コルーチンでなくUpdate()内で動かせたら理想
    public IEnumerator SwitchDirectionPattern1()
    {
        // 上→下→右→左
        while (true)
        {
            TurnFront();
            yield return new WaitForSeconds(switchSpeed);

            TurnBack();
            yield return new WaitForSeconds(switchSpeed);

            TurnRight();
            yield return new WaitForSeconds(switchSpeed);

            TurnLeft();
            yield return new WaitForSeconds(switchSpeed);

        }

    }

    void TurnFront()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.down);
        Debug.Log("front");
        enemyAnimator.SetTrigger("TurnFront");
    }

    void TurnBack()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.up);
        Debug.Log("back");
        enemyAnimator.SetTrigger("TurnBack");
    }

    void TurnRight()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.right);
        Debug.Log("right");
        enemyAnimator.SetTrigger("TurnRight");
    }

    void TurnLeft()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.left);
        Debug.Log("left");
        enemyAnimator.SetTrigger("TurnLeft");
    }

    void Attack()
    {
        playerController.isInStage = false;
        Debug.Log("game over");

    }

    void MovePlayerSide()
    {

    }

}
