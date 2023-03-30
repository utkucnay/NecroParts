using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    MoveCommandTransform moveCommandTransform;
    RotateCommand rotateCommand;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        moveCommandTransform = new MoveCommandTransform(transform, Vector2.zero);
        rotateCommand = new RotateCommand(spriteRenderer,Vector2.zero);
    }

    public void MoveDir(Vector2 velocity)
    {
        moveCommandTransform._velocity = velocity;
        CommandStream.AddCommand(moveCommandTransform);
    }

    public void RotateDir(Vector2 dir)
    {
        rotateCommand._dir = dir;
        CommandStream.AddCommand(rotateCommand);
    }
}
