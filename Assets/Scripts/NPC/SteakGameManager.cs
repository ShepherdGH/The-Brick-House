using UnityEngine;
using TMPro; 
using System.Collections;

public class SteakGameManager : MonoBehaviour
{
    [Header("Game Params")]
    [SerializeField] private int winningScore = 10;
    [SerializeField] private string playerOneTag = "Player";
    [SerializeField] private string playerTwoTag = "Player";
    
    [Header("Player Ref")]
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    
    [Header("UI Params")]
    [SerializeField] private TMP_Text player1ScoreText; 
    [SerializeField] private TMP_Text player2ScoreText; 
    [SerializeField] private GameObject winnerPanel;
    [SerializeField] private TMP_Text winnerText;
    
    private int p1Score = 0;
    private int p2Score = 0;
    private bool gameOver = false;
    
    public static SteakGameManager Instance {get; private set;}
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        if (player1 == null)
        {
            player1 = GameObject.Find("Player1");
        }
        if (player2 == null)
        {
            player2 = GameObject.Find("Player2");
        }
        if (winnerPanel != null)
        {
            winnerPanel.SetActive(false);
        }
        UpdateScore();
    }
    
    public void AddScore(GameObject playerObject)
    {
        if (gameOver) return;
        if (playerObject == player1)
        {
            p1Score++;
        }
        else if (playerObject == player2)
        {
            p2Score++;
        }
        UpdateScore();
        FindWinner();
    }
    
    private void UpdateScore()
    {
        if (player1ScoreText != null)
        {
            player1ScoreText.text = "Player 1: " + p1Score;
        }
        if (player2ScoreText != null)
        {
            player2ScoreText.text = "Player 2: " + p2Score;
        }
    }
    
    private void FindWinner()
    {
        if (p1Score >= winningScore)
        {
            GameOver("Player 1 Wins!");
        }
        else if (p2Score >= winningScore)
        {
            GameOver("Player 2 Wins!");
        }
    }
    
    private void GameOver(string winnerMessage)
    {
        Time.timeScale = 0;
        gameOver = true;
        if (winnerPanel != null)
        {
            winnerPanel.SetActive(true);
            
            if (winnerText != null)
            {
                winnerText.text = winnerMessage;
            }
        }
    }

    public void ResetGame()
    {
        p1Score = 0;
        p2Score = 0;
        gameOver = false;
        
        UpdateScore();
        
        if (winnerPanel != null)
        {
            winnerPanel.SetActive(false);
        }
    }
}