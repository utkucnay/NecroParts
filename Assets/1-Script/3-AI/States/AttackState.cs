using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : BaseState
{
    protected AttackData attackData;

    public GameObject targetObject;

    public AttackState(StateMachine stateMachine, AttackData attackData) : base(stateMachine)
    {
        this.attackData = attackData;
    }

    protected override void StartState()
    {
        base.StartState();
    }

    protected override void UpdateState()
    {
        base.UpdateState();
    }
    protected override void OnEnd()
    {
        base.OnEnd();
    }

    protected bool IsValidTarget()
    {
        if (targetObject == null) return false;
        return true;
    }
}