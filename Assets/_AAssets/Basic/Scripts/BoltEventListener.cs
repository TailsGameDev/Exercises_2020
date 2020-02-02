using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UdpKit;

public class BoltEventListener : Bolt.GlobalEventListener
{
    [SerializeField] string gameScene;

    public override void BoltStartDone()
    {
        if (BoltNetwork.IsServer)
        {
            string matchName = Guid.NewGuid().ToString();

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
                BoltNetwork.Connect(photonSession);
            }
        }
    }
}
