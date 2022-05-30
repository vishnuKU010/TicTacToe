using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviourPun
{

    public PlayerController leftPlayer;
    public PlayerController rightPlayer;
    //public PlayerController currentPlayer;
    //public PlayerController localPlayer;
    public GameObject endGameState;
    public int currentTurn = 0;
    private int moveCount;

    [SerializeField] Sprite tilePlayerO;
    [SerializeField] Sprite tilePlayerX;
    [SerializeField] Button[] tileButton;
    [SerializeField] Text[] tileList;
    [SerializeField] Text winnerText;


    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
            SetPlayres();

    }

    void SetPlayres()
    {
        leftPlayer.photonView.TransferOwnership(1);
        rightPlayer.photonView.TransferOwnership(2);

        leftPlayer.photonView.RPC("Initialize", RpcTarget.AllBuffered, PhotonNetwork.CurrentRoom.GetPlayer(1));
        rightPlayer.photonView.RPC("Initialize", RpcTarget.AllBuffered, PhotonNetwork.CurrentRoom.GetPlayer(2));
        leftPlayer.playerName.text = PhotonNetwork.CurrentRoom.GetPlayer(1).NickName;
        rightPlayer.playerName.text = PhotonNetwork.CurrentRoom.GetPlayer(2).NickName;
        //photonView.RPC("NextTurn", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void NextTurn()
    {
        if (currentTurn == 0)
            currentTurn = 1;
        else
            currentTurn = 0;
        photonView.RPC("SetTurn", RpcTarget.Others, currentTurn);
    }

    [PunRPC]
    public void SetTurn(int index)
    {
        currentTurn = index;
    }

    [PunRPC]
    public void TileTapped(int index)
    {
        if (currentTurn == 0)
        {
            tileButton[index].GetComponent<TileController>().internalText.text = "X";
            tileButton[index].interactable = false;
            tileButton[index].image.sprite = tilePlayerX;
        }
        else
        {
            tileButton[index].GetComponent<TileController>().internalText.text = "O";
            tileButton[index].interactable = false;
            tileButton[index].image.sprite = tilePlayerO;
        }
        
        EndTurn();
    }

    public void EndTurn()
    {
        moveCount++;
        string str = "X";
        if (PlayerController.me.playerIndex == 0)
            str = "X";
        else
            str = "O";
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (tileList[0].text == str && tileList[1].text == str && tileList[2].text == str) GameOver(str);
            else if (tileList[3].text == str && tileList[4].text == str && tileList[5].text == str) GameOver(str);
            else if (tileList[6].text == str && tileList[7].text == str && tileList[8].text == str) GameOver(str);
            else if (tileList[0].text == str && tileList[3].text == str && tileList[6].text == str) GameOver(str);
            else if (tileList[1].text == str && tileList[4].text == str && tileList[7].text == str) GameOver(str);
            else if (tileList[2].text == str && tileList[5].text == str && tileList[8].text == str) GameOver(str);
            else if (tileList[0].text == str && tileList[4].text == str && tileList[8].text == str) GameOver(str);
            else if (tileList[2].text == str && tileList[4].text == str && tileList[6].text == str) GameOver(str);
            else if (moveCount >= 9) GameOver("D");
            else
                NextTurn();
        }
        else if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (tileList[0].text == str && tileList[1].text == str && tileList[2].text == str && tileList[3].text == str) GameOver(str);
            else if (tileList[4].text == str && tileList[5].text == str && tileList[6].text == str && tileList[7].text == str) GameOver(str);
            else if (tileList[8].text == str && tileList[9].text == str && tileList[10].text == str && tileList[11].text == str) GameOver(str);
            else if (tileList[12].text == str && tileList[13].text == str && tileList[14].text == str && tileList[15].text == str) GameOver(str);
            else if (tileList[0].text == str && tileList[4].text == str && tileList[8].text == str && tileList[12].text == str) GameOver(str);
            else if (tileList[1].text == str && tileList[5].text == str && tileList[9].text == str && tileList[13].text == str) GameOver(str);
            else if (tileList[2].text == str && tileList[6].text == str && tileList[10].text == str && tileList[14].text == str) GameOver(str);
            else if (tileList[3].text == str && tileList[7].text == str && tileList[11].text == str && tileList[15].text == str) GameOver(str);
            else if (tileList[0].text == str && tileList[5].text == str && tileList[10].text == str && tileList[15].text == str) GameOver(str);
            else if (tileList[3].text == str && tileList[6].text == str && tileList[9].text == str && tileList[12].text == str) GameOver(str);
            else if (moveCount >= 9) GameOver("D");
            else
                NextTurn();
        }
        else 
        {
            if (tileList[0].text == str && tileList[1].text == str && tileList[2].text == str && tileList[3].text == str && tileList[4].text == str) GameOver(str);
            else if (tileList[5].text == str && tileList[6].text == str && tileList[7].text == str && tileList[8].text == str && tileList[9].text == str) GameOver(str);
            else if (tileList[10].text == str && tileList[11].text == str && tileList[12].text == str && tileList[13].text == str && tileList[14].text == str) GameOver(str);
            else if (tileList[15].text == str && tileList[16].text == str && tileList[17].text == str && tileList[18].text == str && tileList[19].text == str) GameOver(str);
            else if (tileList[20].text == str && tileList[21].text == str && tileList[22].text == str && tileList[23].text == str && tileList[24].text == str) GameOver(str);
            else if (tileList[0].text == str && tileList[5].text == str && tileList[10].text == str && tileList[15].text == str && tileList[20].text == str) GameOver(str);
            else if (tileList[1].text == str && tileList[6].text == str && tileList[11].text == str && tileList[16].text == str && tileList[21].text == str) GameOver(str);
            else if (tileList[2].text == str && tileList[7].text == str && tileList[12].text == str && tileList[17].text == str && tileList[22].text == str) GameOver(str);
            else if (tileList[3].text == str && tileList[8].text == str && tileList[13].text == str && tileList[18].text == str && tileList[23].text == str) GameOver(str);
            else if (tileList[4].text == str && tileList[9].text == str && tileList[14].text == str && tileList[19].text == str && tileList[24].text == str) GameOver(str);
            else if (tileList[0].text == str && tileList[6].text == str && tileList[12].text == str && tileList[18].text == str && tileList[24].text == str) GameOver(str);
            else if (tileList[4].text == str && tileList[8].text == str && tileList[12].text == str && tileList[16].text == str && tileList[20].text == str) GameOver(str);
            else if (moveCount >= 9) GameOver("D");
            else
                NextTurn();
        }

    }

    private void GameOver(string winningPlayer)
    {

        //ToggleButtonState(false);
        photonView.RPC("GameOverRPC", RpcTarget.AllBuffered, winningPlayer);
    }

    [PunRPC]
    public void GameOverRPC(string str)
    {
        switch (str)
        {
            case "D":
                winnerText.text = "DRAW";
                break;
            case "X":
                winnerText.text = PhotonNetwork.CurrentRoom.GetPlayer(1).NickName;
                break;
            case "O":
                winnerText.text = PhotonNetwork.CurrentRoom.GetPlayer(2).NickName;
                break;
        }
        endGameState.SetActive(true);
    }

    public void LoadHome()
    {
        SceneManager.LoadScene(0);
    }
}
