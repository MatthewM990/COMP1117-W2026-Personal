using Unity.VisualScripting;
using UnityEngine;

public class PlayerCrouchState : PlayerBaseState
{
    private Vector2 originSize;
    public override void EnterState(Player player)
    {
        var collider = player.GetComponent<CapsuleCollider2D>();
        originSize = collider.size;

        collider.size = new Vector2(originSize.x, originSize.y * 0.1f);
    }

    public override void ExitState(Player player)
    {
        var collider = player.GetComponent<CapsuleCollider2D>();
        collider.size = originSize;
    }

    public override void FixedUpdateState(Player player)
    {
        player.rBody.linearVelocity = new Vector2(player.moveInput.x * (player.data.moveSpeed * 0.5f), player.rBody.linearVelocityY);

        player.FlipSprite(player.moveInput.x);
    }

    public override void UpdateState(Player player)
    {
        Bounds bounds = player.GetComponent<CapsuleCollider2D>().bounds;

        Vector2 Head = new Vector2(bounds.center.x, bounds.max.y + 2f);

        bool blockedAbove = Physics2D.OverlapCircle(Head, 0.2f);
        bool standing = player.moveInput.y >= 0f;

        if (standing && !blockedAbove)
        {
            player.SwitchState(player.GroundedState);   
        }


        if (!player.CheckGrounded())
        {
            player.SwitchState(player.AirborneState);
        }

    }
}
