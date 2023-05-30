using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class FollowAttackTargetState : AttackState
{
    RandomMoveData randomMoveData;
    Skeleton skeleton;

    public FollowAttackTargetState(StateMachine stateMachine) : base(stateMachine)
    {
        randomMoveData = stateMachine.GetData<RandomMoveData>("randomMoveData");
    }

    public override void InitState()
    {
        base.InitState();
        skeleton = gameObject.GetComponent<Skeleton>();
    }

    protected override void StartState()
    {
        base.StartState();
    }

    protected override void UpdateState()
    {
        base.UpdateState();

        if (IsValidTarget())
        {
            var dir = Vector3.Normalize(targetObject.transform.position - transform.position);
            //move.Velocity = targetObject.transform.position - transform.position;

            skeleton.MoveDir(dir * randomMoveData.speed);
            skeleton.RotateDir(dir);

            if (Vector2.Distance(targetObject.transform.position, transform.position) <= attackData.attackInRange * multipler.attackRangeMultipler)
            {
                stateMachine.GetState<AttackTargetState>().targetObject = targetObject;
                stateMachine.ChangeState(stateMachine.GetState<AttackTargetState>());
            }
        }
        else
        {
            stateMachine.ChangeState(stateMachine.GetState<RandomMoveState>());
        }


        var targetPoints = AIManager.s_Instance.CalculateTargetPointsForPos(transform.position, randomMoveData.attackRange, 20);
        CalculateTargetPoints(targetPoints, Player.s_Instance.targetEnemyPoints);
        int maxValue = int.MinValue, index = -1;

        for (int i = 0; i < targetPoints.Length; i++)
        {
            if (maxValue < targetPoints[i] && targetPoints[i] > 0)
            {
                index = i;
                maxValue = targetPoints[i];
            }
        }

        if (index != -1)
        {
            targetObject = AIManager.s_Instance.GetEnemy(index).gameObject;
        }
        else
        {
            stateMachine.ChangeState(stateMachine.GetState<RandomMoveState>());
        }
    }

    protected override void OnEnd()
    {
        base.OnEnd();
    }

    void CalculateTargetPoints(int[] targetPoints, int[] playerTargetPoints)
    {
        int length = targetPoints.Length;

        for (int i = 0; i < length; i++)
        {
            if (playerTargetPoints[i] > 0)
            {
                targetPoints[i] = targetPoints[i] + playerTargetPoints[i];
            }
            else
            {
                targetPoints[i] = 0;
            }
        }
    }
}