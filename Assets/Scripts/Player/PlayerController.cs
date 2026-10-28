using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using System.Collections;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public float jumpBufferTime = 0.15f;
    public float speed = 5f;
    public UnityEngine.Transform SpawnPoint;
    private bool canControl = true;

    public float resetJumpTime = 0.15f;
    public float jumpForce = 5f;
    public Rigidbody2D rdb = null;
    private Coroutine resetCoroutine;
    private int bred;
    private bool canJump = true;
    bool jumpRequested = false;
    private float jumpBufferCounter = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rdb = GetComponent<Rigidbody2D>();
    }
    IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(resetJumpTime);
        Debug.Log("canJump = false;");
        canJump = false;
        resetCoroutine = null;
    }
    void StartJump()
    {
        rdb.linearVelocity = new Vector2(
        rdb.linearVelocity.x,
        jumpForce
        );
        canJump = false;
        jumpBufferCounter = 0;
    }
    public void SetControlEnabled(bool enabled)
    {
        canControl = enabled;

        if (!enabled)
        {
            rdb.linearVelocity = new Vector2(
                0f,
                rdb.linearVelocity.y
            );
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!canControl)
            return;
        float horizontal = Input.GetAxisRaw("Horizontal");
        //transform.position = new Vector3(transform.position.x + speed*horizontal*Time.deltaTime, transform.position.y, transform.position.z);
        //transform.position = new Vector3(transform.position.x*(-horizontal) + speed * horizontal, transform.position.y, transform.position.z);
        //Debug.Log(horizontal);
        //Debug.Log(transform.position);

        rdb.linearVelocity = new Vector2(horizontal * speed, rdb.linearVelocity.y);
        //rdb.AddForce(new Vector2(horizontal * speed, rdb.linearVelocity.y));
        //Debug.Log("rdb.linearVelocity=" + rdb.linearVelocity);
        //Debug.Log("Time.deltaTime=" + Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;

        }
        if (canJump && jumpBufferCounter > 0)
        {
            StartJump();
        }
        if (jumpBufferCounter > 0)
        {
            jumpBufferCounter = Mathf.Max(
                0f,
                jumpBufferCounter - Time.deltaTime
            );
        }

        if (transform.position.y< -8)
        {
            transform.position = SpawnPoint.position;
            rdb.linearVelocity = new Vector2();
        }
        //Debug.Log("Current transform=" + transform.position);

        //transform.rotation = new Quaternion();
    }
    void OnCollisionEnter2D(Collision2D collision)

    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                //Debug.Log("Normal: " + contact.normal);
                if (contact.normal.y > 0)
                {
                    canJump = true;
                    // Отменяем таймер, если он запущен
                    if (resetCoroutine != null)
                    {
                        StopCoroutine(resetCoroutine);
                        resetCoroutine = null;
                    }
                }
            }
            bred += 1;
            //Debug.Log("AAAAAAAA=" + bred);
            
        }
    }
    void OnCollisionExit2D(Collision2D collision)

    {
        if (collision.gameObject.CompareTag("Ground") && resetCoroutine == null)
        {
            resetCoroutine = StartCoroutine(ResetJump());
        }
    }
}
