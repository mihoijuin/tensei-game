using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDirector : MonoBehaviour {


	void Start () {
        switch (ItemDirector.PointState)
        {
            case ItemDirector.POINTSTATE.GOOD:
                Debug.Log("good");
                break;
            case ItemDirector.POINTSTATE.NORMAL:
                Debug.Log("normal");
                break;
            case ItemDirector.POINTSTATE.BAD:
                Debug.Log("bad");
                break;
            default:
                Debug.Log("error");
                break;
        }
    }
}
