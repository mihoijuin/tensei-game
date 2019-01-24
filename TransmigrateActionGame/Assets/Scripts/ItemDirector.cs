using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDirector : MonoBehaviour {

    public int currentPoint;

    public int initialPoint;

    public int changeAmount; 


	void Start () {
        // 初期化
        InitPoint();
	}

    void InitPoint()
    {
        currentPoint = initialPoint;
    }

    public void CountUpPoint()
    {
        currentPoint += changeAmount;
    }


    public void CountDownPoint()
    {
        currentPoint -= changeAmount;
    }
}
