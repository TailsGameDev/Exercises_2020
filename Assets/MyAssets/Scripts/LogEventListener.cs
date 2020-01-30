using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogEventListener : Bolt.GlobalEventListener
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            var logEvent = LogEvent.Create();
            logEvent.Message = "space pressed!";
            logEvent.Send();
        }
    }

    public override void OnEvent(LogEvent evnt)
    {
        Destroy(Camera.main.gameObject);
    }

    public override void OnEvent(MyCustomEvent evnt)
    {
        // you can make your events, create and listen to them.
    }
}
