using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigid;
    private BoxCollider2D col;

    private Animator anim;

    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float fallMultiplier;

    private bool isCanJump = true;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Jump();
        GroundCheck();
    }

    public void Move()
    {
        if (rigid == null) return;

        float dirH = Input.GetAxisRaw("Horizontal");
        rigid.velocity = Vector2.right * dirH * speed + Vector2.up * rigid.velocity.y;

        if (rigid.velocity.y < 0)
            rigid.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.deltaTime;
    }

    public void Jump()
    {
        if (rigid == null || !isCanJump) return;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            isCanJump = false;
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    public void GroundCheck()
    {
        if (rigid.velocity.y > 0 || isCanJump) return;

        var hit = Physics2D.BoxCast(transform.position, Vector2.one, 0, Vector2.down, 0.1f, 1 << 8);
        if (hit.collider != null)
        {
            Debug.Log(hit);
            isCanJump = true;
        }
    }

    private void OnDrawGizmos()
    {
        
    }

}
