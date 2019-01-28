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

    public static POINTSTATE PointState { get; private set; }


    void Start()
    {
        // 初期化
        InitPoint();
    }


    public void SwitchState()
    {
        if(currentPoint <= -thresholdPoint) { PointState = POINTSTATE.BAD; }
        else if(currentPoint >= thresholdPoint) { PointState = POINTSTATE.GOOD; }
        else { PointState = POINTSTATE.NORMAL; }
    }


    void InitPoint()
    {
        currentPoint = initialPoint;
        thresholdPoint = changeAmount * 2;
        PointState = POINTSTATE.NORMAL;
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
