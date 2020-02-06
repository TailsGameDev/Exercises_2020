using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UdpKit;

public class BoltEventListener : Bolt.GlobalEventListener
{
    public string sessionID;
    [SerializeField] string gameScene;

    public void SetSessionID(string id)
    {
        sessionID = id;
    }

    public override void BoltStartDone()
    {
        if (BoltNetwork.IsServer)
        {
            string matchName = //Guid.NewGuid().ToString();
                                sessionID;

            Bolt.Matchmaking.BoltMatchmaking.CreateSession(
                sessionID: matchName,
                sceneToLoad: gameScene
            );
        }
    }

    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    {
        foreach (var session in sessionList)
        {
            UdpSession photonSession = session.Value as UdpSession;

            if (photonSession.Source == UdpSessionSource.Photon)
            {
                if (photonSession.HostName.ToString() == sessionID)
                {
                    Debug.LogError(photonSession.HostName.ToString() + "<- id");
                    BoltNetwork.Connect(photonSession);
                }
            }
        }
    }
}
