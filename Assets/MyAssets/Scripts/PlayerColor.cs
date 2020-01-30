using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : Bolt.EntityBehaviour<IMyPlayerState>
{

    [SerializeField] MeshRenderer meshRenderer;

    public override void Attached()
    {
        state.AddCallback("MyColor", ColorChanged);

        if (entity.IsOwner)
        {
            state.MyColor = new Color(Random.value, Random.value, Random.value);
        }

    }

    void ColorChanged()
    {
        meshRenderer.material.color = state.MyColor;
    }

}
