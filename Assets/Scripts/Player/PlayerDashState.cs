using System.Threading;
using UnityEngine;

public class PlayerDashState : PlayerBaseState
{
    private float dashTime = 0.2f;
    private float dashTimer;
    private float dashSpeed = 15f;
    private Vector2 dashDirection;
    

    public override void EnterState(Player player)
    {
        dashTimer = dashTime;
        float lookDir = Mathf.Sign(player.anim.transform.localScale.x);

        dashDirection = new Vector2(Mathf.Sign(lookDir), 0f);

        player.rBody.gravityScale = 0f;

        player.rBody.linearVelocity = dashDirection * dashSpeed;
    }

    public override void ExitState(Player player)
    {
        Time.timeScale = 1f;
        player.rBody.gravityScale = 5f;
    }

    public override void FixedUpdateState(Player player)
    {
        player.rBody.linearVelocity = dashDirection * dashSpeed;
    }

    public override void UpdateState(Player player)
    {
        dashTimer -= Time.deltaTime;

        if(dashTimer <= 0f)
        {
            player.rBody.gravityScale = 5f;

            if(player.CheckGrounded())
            {
                player.SwitchState(player.GroundedState);
            }
            else
            {
                player.SwitchState(player.AirborneState);
            }
                
        }
    }
}
