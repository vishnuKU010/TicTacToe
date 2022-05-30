using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPun
{
    public Player photonPlayer;
    public static PlayerController me;
    public static PlayerController enemy;
    public int playerIndex;
    public Text playerName;

    [PunRPC]
    public void Initialize(Player player)
    {
        photonPlayer = player;
        if(player.IsLocal)
        {
            me = this;
        }
        else
        {
            enemy = this;
        }
    }
}
