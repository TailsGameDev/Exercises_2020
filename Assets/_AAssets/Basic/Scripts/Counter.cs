using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bolt.Photon;

public class Counter : Bolt.GlobalEventListener
{

    [SerializeField] Text counterText;

    

    public override void OnEvent(CountResponse evnt)
    {
        counterText.text = evnt.count + "";
    }
}
