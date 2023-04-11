using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpanWaitTime : WaitState
{
    SkeletonProp skeletonProp;

    public SpanWaitTime(StateMachine stateMachine, SkeletonProp skeletonProp) : base(stateMachine)
    {
        this.skeletonProp = skeletonProp;
    }

    public override void InitState()
    {
        base.InitState();
        goState = stateMachine.GetState<RandomMoveState>();
    }

    protected override void StartState()
    {
        base.StartState();
        waitTime = skeletonProp.afterSpawnTime;
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
