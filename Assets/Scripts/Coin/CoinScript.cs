
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private GameController gameController;
    private bool collected = false;

    void Awake()
    {
        gameController = FindAnyObjectByType<GameController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (collected)
            return;

        collected = true;

        if (gameController != null)
        {
            gameController.AddCoin();
        }
        else
        {
            Debug.LogError("GameController not found!");
        }

        Destroy(gameObject);
    }
}
