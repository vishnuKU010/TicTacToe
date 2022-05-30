using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class TileController : MonoBehaviourPun
{
    [Header("Component References")]
    //public GameStateController gameController;
    public PlayerManager playerManager;
    public Button interactiveButton;                                 
    public Text internalText;
    [SerializeField] int tileNumber;

    public void UpdateTile()
    {
        //internalText.text = gameController.GetPlayersTurn();
        //interactiveButton.image.sprite = gameController.GetPlayerSprite();
        //interactiveButton.interactable = false;
        //gameController.EndTurn();
        //photonView.RPC("LockTile", RpcTarget.AllBuffered);
        Debug.LogError(PlayerController.me.playerIndex+" : "+ playerManager.currentTurn);
        if (PlayerController.me.playerIndex != playerManager.currentTurn)
            return;
        playerManager.photonView.RPC("TileTapped", RpcTarget.AllBuffered, tileNumber); ;
    }

    //public void LockTile()
    //{
    //    interactiveButton.image.sprite = playerManager.GetPlayerSprite();
    //}

    public void ResetTile()
    {
        //internalText.text = "";
        //interactiveButton.image.sprite = gameController.tileEmpty;
    }
}