using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private void LateUpdate()
    {
        Vector2 pos = Player.s_Instance.transform.position;
        transform.position = new Vector3(pos.x, pos.y, -70 + pos.y);
    }
}
