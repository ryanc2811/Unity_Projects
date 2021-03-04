using GameEngine.Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchForTarget : BaseCommand
{
    private float speed = 5f;

    private bool movingRight = true;
    private float groundDistance=1f;
    private float wallDistance = 0.5f;
    Transform groundDetection;
    Transform wallDetection;
    public override void StartCommand()
    {
        groundDetection = ((IPatrolAI)owner).GroundDetection;
        wallDetection = ((IPatrolAI)owner).WallDetection;
        owner.Anim.SetTrigger("Move");
    }
    public override void Execute()
    {
        owner.Transform.Translate(Vector2.right * speed * Time.deltaTime);
        #region GroundDetection
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, groundDistance, LayerMask.GetMask("Ground"));
        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                owner.Transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                owner.Transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
        #endregion
        #region WallDetection
        if (movingRight == true)
        {
            Vector3 origin = wallDetection.position;
            Vector3 dir = Vector2.right;
            RaycastHit2D hit = Physics2D.Raycast(origin, dir, wallDistance, LayerMask.GetMask("Ground"));
            if (hit.collider != null)
            {
                movingRight = false;
                owner.Transform.eulerAngles = new Vector3(0, -180, 0);
                Debug.DrawLine(origin, hit.point, Color.red);
            }
            else Debug.DrawLine(origin, origin + dir * wallDistance, Color.white, 0.01f);
        }
        else
        {
            Vector3 origin = wallDetection.position;
            Vector3 dir = -Vector2.right;
            RaycastHit2D hit = Physics2D.Raycast(origin, dir, wallDistance,LayerMask.GetMask("Ground"));
            if (hit.collider != null)
            {
                movingRight = true;
                owner.Transform.eulerAngles = new Vector3(0, 0, 0);
                Debug.DrawLine(origin, hit.point, Color.red);
            }
            else Debug.DrawLine(origin, origin + dir * wallDistance, Color.white, 0.01f);
        }
        #endregion
    }
    public override void ExitCommand()
    {
        owner.Anim.ResetTrigger("Move");
    }
}
