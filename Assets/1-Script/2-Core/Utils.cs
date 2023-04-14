using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public static class Utils
{
    public static float Scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {

        float OldRange = OldMax - OldMin;
        float NewRange = NewMax - NewMin;
        float NewValue = ((OldValue - OldMin) * NewRange / OldRange) + NewMin;

        return NewValue;
    }

    public static bool InBox(Vector2 pos, Vector2 TopRight, Vector2 BotLeft, float treshold = 0)
    {
        TopRight += (Vector2.one * treshold);
        BotLeft -= (Vector2.one * treshold);
        if (pos.x < TopRight.x && pos.x > BotLeft.x && pos.y < TopRight.y && pos.y > BotLeft.y) return true;
        return false;
    }

    public static bool InMainCamera(Vector2 pos)
    {
        return InBox(pos,
            Camera.main.ScreenToWorldPoint(new Vector3(UnityEngine.Screen.width, UnityEngine.Screen.height, -Camera.main.transform.position.z)),
            Camera.main.ScreenToWorldPoint(new Vector3(0, 0, -Camera.main.transform.position.z)),
            -2f
            );
    }
}
