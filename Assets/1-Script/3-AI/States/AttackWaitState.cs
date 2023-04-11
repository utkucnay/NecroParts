using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWaitState : WaitState
{
    AttackData attackData;

    public AttackWaitState(StateMachine stateMachine, AttackData attackData) : base(stateMachine)
    {
       this.attackData = attackData;
    }

    public override void InitState()
    {
        base.InitState();
        goState = stateMachine.GetState<RandomMoveState>();
    }

    protected override void StartState()
    {
        base.StartState();
        waitTime = attackData.waitTime;
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
