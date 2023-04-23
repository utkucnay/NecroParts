using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : BaseState
{
    protected AttackData attackData;
    protected SkeletonMultipler multipler;

    public GameObject targetObject;

    public AttackState(StateMachine stateMachine) : base(stateMachine)
    {
        attackData = stateMachine.GetData<AttackData>("attackData");
        multipler = stateMachine.GetData<SkeletonMultipler>("skeletonMultipler");
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