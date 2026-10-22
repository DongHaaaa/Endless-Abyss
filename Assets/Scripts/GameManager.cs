using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Score UI")]
    [SerializeField] private TextMeshPro scoreText;
    [SerializeField] private Transform player;

    //Giúp script khác gọi đc gameover, v.v  
    public static GameManager Instance{get; private set;} //Get để đọc giá trị của Instance, private set để cho mỗi class này thay giá trị của nó
    private bool isGameOver;
    private float deepestY;
    public int Score { get; private set; }

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

    private void Start()
    {
        if(player == null)
        {
            Debug.LogError("GameManager: Chua gan Player!");
            return;
        }

        deepestY = player.position.y;
        Score = Mathf.FloorToInt(Mathf.Abs(deepestY));
    }

    private void Update()
    {
        UpdateScore();

        if(isGameOver && Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            RestartGame();
        }
    }

    private void UpdateScore()
    {
        if(isGameOver || player == null) return;

        if(player.position.y < deepestY)
        {
            deepestY = player.position.y;
        }

        Score = Mathf.FloorToInt(Mathf.Abs(deepestY));

        if(scoreText != null)
        {
            scoreText.text = $"Score: {Score}";
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