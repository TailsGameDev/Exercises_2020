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
        var spawnPosition = new Vector3(Random.Range(-6, 6), 0, Random.Range(0, 2));

        BoltNetwork.Instantiate(BoltPrefabs.BoltTank, spawnPosition, Quaternion.identity);
    }

    public override void OnEvent(NewPlayer evnt)
    {
        GameObject[] tanks = GameObject.FindGameObjectsWithTag("tank");

        if (tanks.Length > 1)
        {
            Complete.GameManager.instance.Begin();
        }
    }
}
