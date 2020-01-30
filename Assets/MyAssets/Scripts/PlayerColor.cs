using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : Bolt.EntityEventListener<IMyPlayerState>
{

    [SerializeField] MeshRenderer meshRenderer;

    public override void Attached()
    {
        state.AddCallback("MyColor", ColorChanged);

        if (entity.IsOwner)
        {
            state.MyColor = makeRandomColor();
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            //var myCustomEvent = MyCustomEvent.Create(entity);
            //myCustomEvent.Send();
            state.MyColor = makeRandomColor();
        }
    }

    void ColorChanged()
    {
        meshRenderer.material.color = state.MyColor;
    }

    /*
    public override void OnEvent(MyCustomEvent evnt)
    {
        state.MyColor = makeRandomColor();
    }
    */

    Color makeRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }
}
