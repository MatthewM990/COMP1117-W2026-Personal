using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public override void EnterState(Player player)
    {
        player.jumpsRemaining = player.data.maxJumps;
        player.anim.SetBool("IsGrounded", true);
    }

    public override void UpdateState(Player player)
    {
        player.anim.SetFloat("HorizontalSpeed", Mathf.Abs(player.rBody.linearVelocityX));

        // TRANSITION LOGIC
        if (player.moveInput.y < 0)
        {
            player.SwitchState(player.CrouchState);
        }

        if(!player.CheckGrounded())
        {
            // Not on the ground!
            player.SwitchState(player.AirborneState);
        }
    }

    public override void FixedUpdateState(Player player)
    {
        // MOVEMENT LOGIC
        player.rBody.linearVelocity = new Vector2(
            player.moveInput.x * player.data.moveSpeed, // X
            player.rBody.linearVelocityY);              // Y

        player.FlipSprite(player.moveInput.x);
    }

    public override void OnJumpPressed(Player player)
    {
        // JUMPING LOGIC
        player.rBody.linearVelocity = new Vector2(
            player.rBody.linearVelocityX, 
            player.data.jumpForce);

        player.anim.SetTrigger("Jump");

        AudioManager.Instance.PlayJump();

        player.jumpsRemaining--;

        player.SwitchState(player.AirborneState);
    }

    public override void ExitState(Player player) {   }
}