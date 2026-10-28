
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public TMP_Text textCountCoint;
    public TMP_Text textWinner;
    public TMP_Text textRestart;
    public int coinCount = 0;
    [SerializeField] private PlayerController playerController;

    private bool isFinished = false;
    private void Start()
    {
        textWinner.text = "";
        textRestart.text = "";
        isFinished = false;
    }
    private void Update()
    {
        if (isFinished && Input.GetKeyDown(KeyCode.R))
            RestartGame();
    }
    public void AddCoin()
    {
        coinCount++;
        Debug.Log("Coins: " + coinCount);
        textCountCoint.text = "Coins: " + coinCount;
        //Debug.Log(text.GetComponent<TextMeshPro>());
    }

    public void SetWinner()
    {
        Debug.Log("Player win!");
        textWinner.text = "You winner!\nYour number coin is "+ coinCount;
        textRestart.text = "Press \"R\" to restart a game!";
        playerController.SetControlEnabled(false);
        isFinished = true;
    }

    public void RestartGame()
    {
        Debug.Log("Try restart a game");
        Scene CurrentScene = SceneManager.GetActiveScene();
        Debug.Log(CurrentScene);
        SceneManager.LoadScene(CurrentScene.name, LoadSceneMode.Single);
    }
}
