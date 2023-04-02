using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackTargetState : AttackState
{
    Skeleton skeleton;

    public AttackTargetState(StateMachine stateMachine, AttackData attackData) : base(stateMachine, attackData)
    {
    }

    public override void InitState()
    {
        base.InitState();
        skeleton = gameObject.GetComponent<Skeleton>();
    }

    protected override void StartState()
    {
        base.StartState();


        if (IsValidTarget())
        {
            var targetPos = targetObject.transform.position;
            var VFXObj = GameObject.Instantiate(attackData.attackVFX);

            VFXObj.transform.position = transform.position;

            switch (attackData.attackType)
            {
                case AttackData.AttackType.Ranged:
                    RangedAttack(VFXObj);
                    break;

                case AttackData.AttackType.Melee:
                    MeleeAttack(VFXObj, targetPos);
                    break;
            }
            //CommandStream.s_Instance.sequencer.AddCommand(new RotateCommand(targetPos, stateMachine.VFX));
            skeleton.RotateDir(targetPos);

        }
        else
        {
            stateMachine.ChangeState(stateMachine.GetState<AttackTargetState>());
        }

        stateMachine.ChangeState(stateMachine.GetState<WaitState>());
    }

    protected override void UpdateState()
    {
        base.UpdateState();


    }

    protected override void OnEnd()
    {
        base.OnEnd();

    }

    void RangedAttack(GameObject VFXObj)
    {
        /*Vector3 dir = targetObject.transform.position - transform.position;
        dir = dir.normalized;

        var projectile = VFXObj.GetComponent<Projectile>();
        projectile.SetProjetileData(new ProjectileData() { damage = (int)(attackData.damage), dir = dir, speed = attackData.attackSpeed });
        VFXObj.SetActive(true);*/
    }

    void MeleeAttack(GameObject slashObj, Vector3 targetPos)
    {
        var distVec = targetPos - transform.position;
        if (distVec.x > 0)
        {
            slashObj.GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        else
        {
            slashObj.GetComponentInChildren<SpriteRenderer>().flipX = false;
        }

        var meleeData = slashObj.GetComponent<AttackSlash>();
        meleeData.damage = (int)(attackData.damage);
        meleeData.SetPos(transform.position);

        var meleeGFX = slashObj.GetComponent<MeleeVFX>();
        meleeGFX.SetGFXData(new MeleeGFXData()
        {
            targetDir = targetPos - transform.position,
            baseDir = distVec.x > 0 ? new Vector2(1, 0): new Vector2(-1, 0),
            scaleAttackBegin = attackData.scaleAttackBegin,
            scaleAttackEnd = Utils.Scale(0, attackData.attackInRange, 2, 1, Vector2.Distance(targetPos, transform.position)),
            time = attackData.time
        });
    }
}