using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankInstantiator : Bolt.GlobalEventListener
{
    public override void SceneLoadLocalDone(string map)
    {
        InstantiatePlayer();
    }

    void InstantiatePlayer()
    {
        var spawnPosition = new Vector3(Random.Range(-16, 16), 0, Random.Range(-16, 16));

        BoltNetwork.Instantiate(BoltPrefabs.BoltTank, spawnPosition, Quaternion.identity);
    }
}
