using UnityEngine;
using System.Collections;

//[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener
{
    public override void SceneLoadLocalDone(string map)
    {
        InstantiatePlayer();
        RequestCounter();
    }

    void InstantiatePlayer()
    {
        var spawnPosition = new Vector3(Random.Range(-16, 16), 0, Random.Range(-16, 16));

        BoltNetwork.Instantiate(BoltPrefabs.player, spawnPosition, Quaternion.identity);
    }

    void RequestCounter()
    {
        CountRequest.Create().Send();
    }
}