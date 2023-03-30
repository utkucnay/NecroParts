using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonSM : StateMachine
{
    public RandomMoveState randomMove;
    public FollowAttackTargetState followAttackTargetState;
    public AttackTargetState attackTargetState;

    public SkeletonSMConfig sSMConfig;

    protected override void Awake()
    {
        base.Awake();

        randomMove = new RandomMoveState(this, sSMConfig.randomMoveData);
        followAttackTargetState = new FollowAttackTargetState(this, sSMConfig.attackData, sSMConfig.randomMoveData);
        attackTargetState = new AttackTargetState(this, sSMConfig.attackData);

        states.Add(randomMove);
        states.Add(followAttackTargetState);
        states.Add(attackTargetState);

        InitAllStates();
    }

    protected override void Start()
    {
        base.Start();

    }

    protected override BaseState GetInitialState()
    {
        return randomMove;
    }


}