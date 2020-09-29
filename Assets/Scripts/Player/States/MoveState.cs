using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveState", menuName = "PlayerState/Move", order = 1)]
public class MoveState : PlayerState
{
    [SerializeField]
    private float movementSpeed = 1.0f;

    protected override void CustomStartState()
    {
    }

    protected override void CustomStateUpdate()
    {
        float multiplier = mirrored ? -1.0f : 1.0f;
        //PlayerController.Instance.transform.position += new Vector3(movementSpeed * Time.deltaTime * multiplier, 0.0f, 0.0f);
        playerRigidBody.velocity = new Vector3(movementSpeed * multiplier, .0f, .0f);
    }
}