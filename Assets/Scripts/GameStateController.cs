﻿using UnityEngine;
using UnityEngine.UI;

public class GameStateController : MonoBehaviour
{
    public Image playerXIcon;                                       
    public Image playerOIcon;                                        
    public InputField player1InputField;                             
    public InputField player2InputField;                             
    public Text winnerText;                                          

    public GameObject endGameState;                                  

    public Sprite tilePlayerO;                                       
    public Sprite tilePlayerX;                                       
    public Sprite tileEmpty;                                         
    public Text[] tileList;                                          

    public Color inactivePlayerColor;                                
    public Color activePlayerColor;                                  
    public string whoPlaysFirst;                                     

    private string playerTurn;                                       
    private string player1Name;                                      
    private string player2Name;                                      
    private int moveCount;                                           

    private void Start()
    {
        playerTurn = whoPlaysFirst;
        if (playerTurn == "X") playerOIcon.color = inactivePlayerColor;
        else playerXIcon.color = inactivePlayerColor;

        player1InputField.onValueChanged.AddListener(delegate { OnPlayer1NameChanged(); });
        player2InputField.onValueChanged.AddListener(delegate { OnPlayer2NameChanged(); });

        player1Name = player1InputField.text;
        player2Name = player2InputField.text;
    }

    public void EndTurn()
    {
        moveCount++;
        if (tileList[0].text == playerTurn && tileList[1].text == playerTurn && tileList[2].text == playerTurn) GameOver(playerTurn);
        else if (tileList[3].text == playerTurn && tileList[4].text == playerTurn && tileList[5].text == playerTurn) GameOver(playerTurn);
        else if (tileList[6].text == playerTurn && tileList[7].text == playerTurn && tileList[8].text == playerTurn) GameOver(playerTurn);
        else if (tileList[0].text == playerTurn && tileList[3].text == playerTurn && tileList[6].text == playerTurn) GameOver(playerTurn);
        else if (tileList[1].text == playerTurn && tileList[4].text == playerTurn && tileList[7].text == playerTurn) GameOver(playerTurn);
        else if (tileList[2].text == playerTurn && tileList[5].text == playerTurn && tileList[8].text == playerTurn) GameOver(playerTurn);
        else if (tileList[0].text == playerTurn && tileList[4].text == playerTurn && tileList[8].text == playerTurn) GameOver(playerTurn);
        else if (tileList[2].text == playerTurn && tileList[4].text == playerTurn && tileList[6].text == playerTurn) GameOver(playerTurn);
        else if (moveCount >= 9) GameOver("D");
        else
            ChangeTurn();
    }

    
    public void ChangeTurn()
    {
       
        playerTurn = (playerTurn == "X") ? "O" : "X";
        if (playerTurn == "X")
        {
            playerXIcon.color = activePlayerColor;
            playerOIcon.color = inactivePlayerColor;
        }
        else
        {
            playerXIcon.color = inactivePlayerColor;
            playerOIcon.color = activePlayerColor;
        }
    }

    private void GameOver(string winningPlayer)
    {
        switch (winningPlayer)
        {
            case "D":
                winnerText.text = "DRAW";
                break;
            case "X":
                winnerText.text = player1Name;
                break;
            case "O":
                winnerText.text = player2Name;
                break;
        }
        endGameState.SetActive(true);
        ToggleButtonState(false);
    }

    public void RestartGame()
    {
        moveCount = 0;
        playerTurn = whoPlaysFirst;
        ToggleButtonState(true);
        endGameState.SetActive(false);

        for (int i = 0; i < tileList.Length; i++)
        {
            tileList[i].GetComponentInParent<TileController>().ResetTile();
        }
    }

    private void ToggleButtonState(bool state)
    {
        for (int i = 0; i < tileList.Length; i++)
        {
            tileList[i].GetComponentInParent<Button>().interactable = state;
        }
    }

    public string GetPlayersTurn()
    {
        return playerTurn;
    }

    public Sprite GetPlayerSprite()
    {
        if (playerTurn == "X") return tilePlayerX;
        else return tilePlayerO;
    }

    public void OnPlayer1NameChanged()
    {
        player1Name = player1InputField.text;
    }

    public void OnPlayer2NameChanged()
    {
        player2Name = player2InputField.text;
    }
}
