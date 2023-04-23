using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackWaitState : WaitState
{
    AttackData attackData;
    SkeletonMultipler skeletonMultipler;

    public AttackWaitState(StateMachine stateMachine) : base(stateMachine)
    {
        attackData = stateMachine.GetData<AttackData>("attackData");
        skeletonMultipler = stateMachine.GetData<SkeletonMultipler>("skeletonMultipler");
    }

    public override void InitState()
    {
        base.InitState();
        goState = stateMachine.GetState<RandomMoveState>();
    }

    protected override void StartState()
    {
        base.StartState();
        waitTime = attackData.waitTime * skeletonMultipler.attackSpeedMultipler;
    }

    protected override void UpdateState()
    {
        base.UpdateState();
    }

    protected override void OnEnd()
    {
        base.OnEnd();
    }
}
