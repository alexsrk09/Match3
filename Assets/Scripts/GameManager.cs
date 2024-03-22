using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject mainScreen;
    [SerializeField] GameObject gameOverScreen;

    public static GameManager Instance;

    public float timeToMatch = 10f;
    public float currentTimeToMatch = 0;
    public Board board;
    public enum GameState
    {
        Idle,
        InGame,
        GameOver
    }

    public GameState gameState;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int Points = 0;
    public UnityEvent OnPointsUpdated;
    // Start is called before the first frame update

    void Update()
    {
        if(gameState == GameState.InGame)
        {
            currentTimeToMatch += Time.deltaTime;
            if(currentTimeToMatch > timeToMatch )
            {
                gameState = GameState.GameOver;
            }
        }
        else if(gameState == GameState.GameOver)
        {
            mainScreen.SetActive(false);
            gameOverScreen.SetActive(true);
        }
    }

    public void AddPoints (int newPoints)
    {
        Points += newPoints;
        OnPointsUpdated?.Invoke();
        currentTimeToMatch = 0;
    } 
}
