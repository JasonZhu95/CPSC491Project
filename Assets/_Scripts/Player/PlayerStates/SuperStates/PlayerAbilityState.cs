using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone;
    protected bool trampolineDetected;
    public bool DashTrampolineCheck { get; private set; }

    private bool isGrounded;

    protected Movement Movement => movement ? movement : core.GetCoreComponent(ref movement);
    protected Movement movement;

    private CollisionSenses CollisionSenses => collisionSenses ? collisionSenses : core.GetCoreComponent(ref collisionSenses);
    private CollisionSenses collisionSenses;

    public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        if (CollisionSenses)
        { 
            isGrounded = CollisionSenses.Ground;
        }
    }

    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        trampolineDetected = player.playerObstacleCollision.trampolineDetected;

        if (isAbilityDone)
        {
            if (isGrounded && Movement?.CurrentVelocity.y < 0.01f)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else
            {
                stateMachine.ChangeState(player.InAirState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public bool DashTrampolineSet() => DashTrampolineCheck = true;
    public bool DashTrampolineSetFalse() => DashTrampolineCheck = false;
}
