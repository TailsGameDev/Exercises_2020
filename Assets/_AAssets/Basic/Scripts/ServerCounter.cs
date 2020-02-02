using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BoltGlobalBehaviour(BoltNetworkModes.Server)]
public class ServerCounter : Bolt.GlobalEventListener
{
    private int qtdPlayers = 0;

    public override void OnEvent(CountRequest evnt)
    {
        AtualizaqtdPlayersPlayersParaTodos(+1);
    }

    public override void Disconnected(BoltConnection connection)
    {
        AtualizaqtdPlayersPlayersParaTodos(-1);
    }

    void AtualizaqtdPlayersPlayersParaTodos(int somar)
    {
        qtdPlayers += somar;

        var countResponse = CountResponse.Create();
        countResponse.count = qtdPlayers;
        countResponse.Send();
    }
}
