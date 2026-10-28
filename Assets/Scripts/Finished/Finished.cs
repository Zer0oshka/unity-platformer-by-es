using UnityEngine;

public class Finished : MonoBehaviour
{
    private GameController gameController;
    void Start()
    {
        gameController = FindAnyObjectByType<GameController>();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("other="+ other);
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("You winner!");
            if (gameController!)
            {
                gameController.SetWinner();
            }
        }
    }
}
