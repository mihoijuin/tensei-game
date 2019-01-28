using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public int enemyPattern;
    public float switchSpeed;

    public float attackInterval;
    public float vanishSpeed;

    Animator enemyAnimator;
    RaycastHit2D hit;

    PlayerController playerController;
    GameObject player;


    StageDirector stageDirector;

    enum DIRECTION
    {
        NONE = -1,

        FRONT = 0,
        BACK,
        LEFT,
        RIGHT,

        NUM
    }

    DIRECTION findPos = DIRECTION.NONE;
    DIRECTION enemyEyeDirection = DIRECTION.NONE;

    IEnumerator switchCoroutine;

    // TODO 最終的にはStartではなく、ステージアクティブになったら以下の処理を行う
    void OnEnable ()
    {
        playerController = FindObjectOfType<PlayerController>();
        player = GameObject.Find("Player");

        stageDirector = FindObjectOfType<StageDirector>();

        enemyAnimator = GetComponent<Animator>();        

        switch (enemyPattern)
        {
            case 1:
                switchCoroutine = SwitchDirectionPattern1();
                StartCoroutine(switchCoroutine);
                break;
            case 2:
                switchCoroutine = SwitchDirectionPattern2();
                StartCoroutine(switchCoroutine);
                break;
            case 3:
                switchCoroutine = SwitchDirectionPattern2();
                StartCoroutine(switchCoroutine);
                break;
        }
    }

    private DIRECTION DetermineFindPos()
    {
        float y = player.transform.position.y - transform.position.y;
        float x = player.transform.position.x - transform.position.x;
        float degree = Mathf.Atan2(y, x) * Mathf.Rad2Deg;

        Debug.Log(degree);
        if (degree > -135f && degree < -45f) { return DIRECTION.FRONT; }

        if (degree > -45f && degree < 45f) { return DIRECTION.RIGHT; }

        if (degree > 45f && degree < 135f) { return DIRECTION.BACK; }

        if (degree > 135f || degree < -135f) { return DIRECTION.LEFT; }

        return DIRECTION.NONE;
    }

    private void Update()
    {
        // ゲームステージ中のみ敵の動きを実行
        // Update内でステージ状況監視する以外思いつかなかった...
        if (stageDirector.stageState != StageDirector.STAGESTATE.INSTAGE) { StopCoroutine(switchCoroutine); }

        // 敵に見つかったらゲームオーバー
        if (hit && hit.collider.CompareTag("Player") && stageDirector.stageState == StageDirector.STAGESTATE.INSTAGE)
        {
            stageDirector.stageState = StageDirector.STAGESTATE.NONE;
            StartCoroutine(Teleportation());
        }

        // 敵にぶつかってもゲームオーバー
        if(player && stageDirector.stageState == StageDirector.STAGESTATE.INSTAGE && (player.transform.position - transform.position).magnitude < playerController.playerRadius * 1.5f)
        {
            stageDirector.stageState = StageDirector.STAGESTATE.NONE;
            StartCoroutine(Attack());
        }
    }

    private void FixedUpdate()
    {
        // Update内でレイキャストを飛ばさないとレイキャスト持続しない
        switch (enemyEyeDirection)
        {
            case DIRECTION.FRONT:
                hit = Physics2D.Raycast(transform.position, Vector2.down);
                break;
            case DIRECTION.BACK:
                hit = Physics2D.Raycast(transform.position, Vector2.up);
                break;
            case DIRECTION.RIGHT:
                hit = Physics2D.Raycast(transform.position, Vector2.right);
                break;
            case DIRECTION.LEFT:
                hit = Physics2D.Raycast(transform.position, Vector2.left);
                break;
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

    public IEnumerator SwitchDirectionPattern2()
    {
        // 上→下→右→左
        while (true)
        {
            TurnBack();
            yield return new WaitForSeconds(switchSpeed);

            TurnLeft();
            yield return new WaitForSeconds(switchSpeed);

            TurnFront();
            yield return new WaitForSeconds(switchSpeed);

            TurnRight();
            yield return new WaitForSeconds(switchSpeed);

        }

    }

    public IEnumerator SwitchDirectionPattern3()
    {
        // 上→下→右→左
        while (true)
        {
            TurnLeft();
            yield return new WaitForSeconds(switchSpeed);

            TurnFront();
            yield return new WaitForSeconds(switchSpeed);

            TurnBack();
            yield return new WaitForSeconds(switchSpeed);

            TurnRight();
            yield return new WaitForSeconds(switchSpeed);

        }

    }

    void TurnFront()
    {
        enemyEyeDirection = DIRECTION.FRONT;
        enemyAnimator.SetTrigger("TurnFront");
    }

    void TurnBack()
    {
        enemyEyeDirection = DIRECTION.BACK;
        enemyAnimator.SetTrigger("TurnBack");
    }

    void TurnRight()
    {
        enemyEyeDirection = DIRECTION.RIGHT;
        enemyAnimator.SetTrigger("TurnRight");
    }

    void TurnLeft()
    {
        enemyEyeDirection = DIRECTION.LEFT;
        enemyAnimator.SetTrigger("TurnLeft");
    }

    IEnumerator Teleportation()
    {                          
        yield return new WaitForSeconds(attackInterval);

        // 消える
        GetComponent<SpriteRenderer>().enabled = false;

        // プレイヤーのそばに移動
        findPos = DetermineFindPos();
        MovePlayerSide();
        enemyAnimator.SetTrigger("TurnFront");  // 攻撃のときは正面を向く
        yield return new WaitForSeconds(vanishSpeed);

        // 現れる
        GetComponent<SpriteRenderer>().enabled = true;

        // 攻撃
        StartCoroutine(Attack());

        yield break;

    }

    IEnumerator Attack()
    {
        // TODO 攻撃モーション

        // プレイヤー消滅
        StartCoroutine(playerController.Die());

        // 消滅まで待機
        yield return new WaitUntil(() => !player);

        // ゲームオーバー
        StartCoroutine(stageDirector.GameOver());

    }

    void MovePlayerSide()
    {
        // プレイヤーを発見した方向によって瞬間移動位置を変える
        switch (findPos)
        {
            case DIRECTION.FRONT:
                transform.position = new Vector3(player.transform.position.x, player.transform.position.y + playerController.playerRadius * 2f, transform.position.z);
                break;
            case DIRECTION.BACK:
                transform.position = new Vector3(player.transform.position.x, player.transform.position.y - playerController.playerRadius * 2f, transform.position.z);
                break;
            case DIRECTION.RIGHT:
                transform.position = new Vector3(player.transform.position.x - playerController.playerRadius * 2f, player.transform.position.y, transform.position.z);
                break;
            case DIRECTION.LEFT:
                transform.position = new Vector3(player.transform.position.x + playerController.playerRadius * 2f, player.transform.position.y, transform.position.z);
                break;
        }
    }

}
