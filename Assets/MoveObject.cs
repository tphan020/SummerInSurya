using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    // Start is called before the first frame update
    RectTransform rectTransform;
    private float onTick = 0;
    public const float TICK_TIMER_MAX = .05f; // 20 ticks a sec
    int TickCount = 0;
    private bool Moving = false;
    private float TimeMoving = 0;
    private Vector3 DirectionMoving = new Vector3(0, 0, 0);

    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        onTick += Time.fixedDeltaTime;
        if (onTick >= TICK_TIMER_MAX)
        {
            onTick -= TICK_TIMER_MAX;
            if (Moving)
            {
                TickCount++;
                if (TickCount >= TimeMoving * 20)
                {
                    Moving = false;
                    TimeMoving = 0;
                    DirectionMoving = new Vector3(0, 0, 0);
                    TickCount = 0;
                }
                else
                {
                    Vector3 aPos = rectTransform.position;
                    aPos.y = aPos.z = 0;
                    aPos.x = DirectionMoving.x * Time.deltaTime * 1000;
                    rectTransform.position += aPos;
                }
            }
        }
    }

    public void StartMoveObject(Vector3 dir, float time)
    {
        TimeMoving = time;
        DirectionMoving = dir;
        Moving = true;
    }
}
