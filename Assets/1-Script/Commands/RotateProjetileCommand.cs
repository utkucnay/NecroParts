using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateProjetileCommand : ICommand
{
    Vector2 baseDir;
    Vector2 dir;
    Transform transform;
    public void Execute()
    {
        var angle = Vector2.SignedAngle(baseDir, dir);
        transform.localEulerAngles = new Vector3(0, 0, angle);
    }

    public RotateProjetileCommand(Vector2 baseDir, Vector2 dir, Transform transform)
    {
        this.baseDir = baseDir; 
        this.dir = dir;
        this.transform = transform;
    }
}
