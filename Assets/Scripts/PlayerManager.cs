using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;
    public SwipeManager swipeManager;

    public static bool isGameStarted;

    public GameObject startingPrompt;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        isGameStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }

        if (swipeManager.Tap)
        {
            isGameStarted = true;
            Destroy(startingPrompt);
        }
    }
}
