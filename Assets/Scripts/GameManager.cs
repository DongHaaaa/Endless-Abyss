using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    //Giúp script khác gọi đc gameover, v.v  
    public static GameManager Instance{get; private set;} //Get để đọc giá trị của Instance, private set để cho mỗi class này thay giá trị của nó
    private bool isGameOver;
    private void Awake()
    {
        //Có GameManager khác tồn tại thì cái này cho nhót
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