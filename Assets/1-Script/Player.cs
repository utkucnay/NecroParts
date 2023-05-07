using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-20)]
public class Player : Unit
{
    public static Player s_Instance = null;

    [SerializeField] float speed;
    [HideInInspector] public int[] targetEnemyPoints;

    float damageTime;

    protected override void Awake()
    {
        base.Awake();

        if (s_Instance == null)
            s_Instance = this;
        else
            Destroy(this);

        material.SetTexture("_WhiteTexture", Texture2D.redTexture);

        EventManager.AddEventAction("Start Game", () => gameObject.SetActive(false));

        EventManager.AddEventAction("Start Run", () => gameObject.SetActive(true));
        EventManager.AddEventAction("End Run", () => gameObject.SetActive(false));
    }

    private void Update()
    {
        MoveUpdate();
        CalculateTargetEnemyPoints();
        UIUpdate();
        OnDamage();
    }

    void UIUpdate()
    {
        HealthBar.s_Instance.UpdateSlider(health, maxHealth);
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
    void OnDamage()
    {
        if (damageTime > Time.time)
        {
            material.SetFloat("_LerpDamage", Mathf.Clamp(Utils.Scale(.5f,.4f,0,.7f, damageTime - Time.time), 0, .7f));
        }
        else
        {
            material.SetFloat("_LerpDamage", Mathf.Clamp(Utils.Scale(0f, .1f, 0.7f, 0, Time.time - damageTime), 0, .7f));
        }
    }


    protected override void Damage(float damage)
    {
        base.Damage(damage);
        damageTime = Time.time + .5f;
    }

    protected override void Death()
    {
        EventManager.InvokeEvent("End Run");
    }
}
