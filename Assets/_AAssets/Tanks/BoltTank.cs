using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltTank : Bolt.EntityBehaviour<ITank>
{

    [SerializeField] Transform tankTransform;
    [SerializeField] Complete.TankMovement tankMovement;
    [SerializeField] TankAudio tankAudio;
    [SerializeField] Complete.TankShooting tankShooting;

    public override void Attached()
    {
        state.SetTransforms(state.transform, tankTransform);
    }

    public override void SimulateOwner()
    {
        ManageMovement();

        ManageShoot();
    }

    void ManageMovement()
    {
        float inputVertical = Input.GetAxis("Vertical1");
        tankMovement.setm_MovementInputValue(inputVertical);
        tankAudio.setm_MovementInputValue(inputVertical);

        float inputHorizontal = Input.GetAxis("Horizontal1");
        tankMovement.setm_TurnInputValue(inputHorizontal);
        tankAudio.setm_TurnInputValue(inputHorizontal);
    }

    void ManageShoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            tankShooting.ChargeShoot();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            tankShooting.ReleaseShoot();
        }
    }
}
