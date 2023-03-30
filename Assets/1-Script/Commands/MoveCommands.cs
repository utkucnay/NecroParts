using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommandTransform : ICommand
{
    public MoveCommandTransform(Transform transform, Vector2 velocity)
    {
        _transform = transform;
        _velocity = GameManager.Translate3D(velocity);
    }

    public void Execute()
    {
        _transform.GetComponent<Rigidbody>().Move(GameManager.Translate3D(_velocity));
    }

    public Transform _transform;
    public Vector3 _velocity;
}

public static class MoveHelper
{
    public static void Move(this Transform transform, Vector3 velocity)
    {
        transform.position += velocity * Time.deltaTime;
    }
    public static void Move(this Rigidbody rb, Vector3 velocity)
    {
        rb.velocity = velocity;
    }
}