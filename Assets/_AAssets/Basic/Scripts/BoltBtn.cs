using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltBtn : Bolt.GlobalEventListener
{
    public void ServerBtn(){
        BoltLauncher.StartServer();
    }

    public void ClientBtn(){
        BoltLauncher.StartClient();
    }
}
