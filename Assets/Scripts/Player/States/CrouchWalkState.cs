using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CrouchWalkState", menuName = "PlayerState/CrouchWalk", order = 1)]
public class CrouchWalkState : PlayerState
{
    [SerializeField]
    private float movementSpeed = 0.5f;

    protected override void CustomStartState()
    {
        // TODO: Change the collider while crouched?
    }

    protected override void CustomStateUpdate()
    {
        float multiplier = mirrored ? -1.0f : 1.0f;
        PlayerController.Instance.transform.position += new Vector3(movementSpeed * Time.deltaTime * multiplier, .0f, .0f);
    }
}