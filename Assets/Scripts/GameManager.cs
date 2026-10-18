using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    //??? sorry but my brain is not understand this line, I just copy from the internet, I will learn more about it later
    public static GameManager Instance{get; private set;}
    private bool isGameOver;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Update()
    {
        if(isGameOver && Keyboard.current.rKey.wasPressedThisFrame)
        {
            RestartGame();
        }
    }

    public void GameOver()
    {
        if(isGameOver) return;
        isGameOver = true;
        Debug.Log("You non, Play again now");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}