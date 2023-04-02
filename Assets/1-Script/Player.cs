using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-20)]
public class Player : Unit
{
    public static Player s_Instance = null;

    [SerializeField] float speed;
    [HideInInspector] public int[] targetEnemyPoints;

    protected override void Awake()
    {
        base.Awake();

        if (s_Instance == null)
            s_Instance = this;
        else
            Destroy(this);

        EventManager.AddEventAction("Start Game", () => gameObject.SetActive(false));

        EventManager.AddEventAction("Start Run", () => gameObject.SetActive(true));
        EventManager.AddEventAction("End Run", () => gameObject.SetActive(false));
    }

    private void Update()
    {
        MoveUpdate();
        UIUpdate();
        CalculateTargetEnemyPoints();
    }

    void UIUpdate()
    {

    }
    void MoveUpdate()
    {
        Vector2 dir = Vector2.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            dir.y += 1; 
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            dir.y -= 1;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            dir.x += 1;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            dir.x -= 1;
        }

        dir = dir.normalized;
        MoveDir(dir * speed);
        RotateDir(dir);
    }
    void CalculateTargetEnemyPoints()
    {
        targetEnemyPoints = AIManager.s_Instance.CalculateTargetPointsForPos(transform.position, 9, 20);
    }

    protected override void Death()
    {
        
    }
}
