using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Transform trs = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    public float speed = 50f;
    public float radius = 3f;

    void Update()
    {
        transform.RotateAround(
            trs.position,
            new Vector3(0, 0, 1),
            speed * Time.deltaTime
        );
    }
}
