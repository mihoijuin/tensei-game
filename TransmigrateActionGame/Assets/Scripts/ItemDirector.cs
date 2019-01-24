using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDirector : MonoBehaviour {

    public int currentPoint;

    public int initialPoint;

    public int changeAmount; 

    int thresholdPoint;

    public enum POINTSTATE
    {
        NORMAL = 0,
        GOOD,
        BAD,

        NUM
    }

    public POINTSTATE pointState;


    void Start()
    {
        // 初期化
        InitPoint();
    }

    public void SwitchState()
    {
        if(currentPoint <= -thresholdPoint) { pointState = POINTSTATE.BAD; }
        else if(currentPoint >= thresholdPoint) { pointState = POINTSTATE.GOOD; }
        else { pointState = POINTSTATE.NORMAL; }
    }


    void InitPoint()
    {
        currentPoint = initialPoint;
        thresholdPoint = changeAmount * 2;
        pointState = POINTSTATE.NORMAL;
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
