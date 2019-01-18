using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float playerSpeed;

    Rigidbody2D playerRigid;
	void Start () {
        playerRigid = GetComponent<Rigidbody2D>();
	}
	
	
	void Update () {
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
        playerRigid.MovePosition(playerRigid.position + Vector2.up * playerSpeed);
    }

    private void MoveDown()
    {
        playerRigid.MovePosition(playerRigid.position + Vector2.down * playerSpeed);
    }
}
