using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltShell : Bolt.EntityBehaviour<IBombState>
{
    public override void Attached()
    {
        state.SetTransforms(state.transform, transform);
    }
}
