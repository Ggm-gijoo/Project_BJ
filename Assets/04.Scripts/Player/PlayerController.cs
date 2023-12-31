using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;
using UnityEngine.SceneManagement;
using Map;

public class PlayerController : MonoBehaviour, IGravity
{
    public static PlayerController instance;

    private Rigidbody2D rigid;
    private BoxCollider2D col;

    private Animator anim;
	[SerializeField] private GameObject deathParticle;
    private SpriteRenderer sprite;

    [SerializeField]
    private Camera ingameCam;

    [SerializeField] private string bullet;
    [SerializeField] private string chargedBullet;

    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private SpriteRenderer eye;
    [SerializeField] private GameObject chargedBody;

    [SerializeField] private LayerMask groundLayerMask;

	private Vector2 moveVelocity;
    private float maxMoveVelocity;

    public bool IsCanJump => isCanJump;
    private Vector3 gravityDir = new Vector3(0,-9.81f,0);
	public Vector3 GravityDir 
    {
        get => gravityDir; 
        set => gravityDir = value; 
    }

	private float fireTimer = 0f;
    private bool isCanMove = true;
    private bool isCanJump = true;
    private bool isDie = false;

    private float moveInput;
    private bool jumpInput;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        //deathParticle = GetComponentInChildren<ParticleSystem>();

        sprite.color = new Color(1, 1, 1, 1);
        eye.color = sprite.color;
        isDie = false;

        instance = this;
	}
    //
    private void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetKeyDown(KeyCode.Space);

        if (isDie)
        {
            isCanMove = false;
		}
        else
        {
            isCanMove = !Marker.MarkerManager.Instance.drawMarker.IsDrawing;
        }

        if(!isCanMove)
        {
            return;
        }
        else
        {
            Jump();
            Fire();
        }
    }

	private void FixedUpdate()
    {
        Gravity();
        GroundCheck();
        if (!isCanMove)
        {
            return;
        }
        else
        {
            Move();
        }
    }

    private void Gravity()
	{
        rigid.AddForce(GravityDir);
	}


	public void Move()
    {
        if (rigid == null) return;

        moveVelocity = Vector2.right * moveInput * speed;

        //rigid.velocity = moveVelocity + Vector2.right * rigid.velocity.x + Vector2.up * rigid.velocity.y;

        rigid.velocity = Vector2.right * Mathf.Lerp(rigid.velocity.x, moveVelocity.x, Time.deltaTime * 3) + Vector2.up * rigid.velocity.y;

        if (rigid.velocity.y < 0)
            rigid.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.deltaTime;
    }

    public void Jump()
    {
		if (rigid == null || !isCanJump) return;

        if(jumpInput)
        {
            isCanJump = false;
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
            rigid.AddForce(-GravityDir.normalized * jumpPower, ForceMode2D.Impulse);
            Sound.SoundManager.Instance.PlayEFF("Jump");
        }
    }

    public void Fire()
    {
        if(!InventoryManager.Instance.inventoryData.isGetGun)
        {
            return;
		}

		if (InventoryManager.Instance.inventoryData.isGetBigGun && fireTimer >= 1f)
		{
			chargedBody.SetActive(true);
		}
		else
		{
			chargedBody.SetActive(false);
		}

		if (Input.GetMouseButton(1))
        {
            fireTimer += Time.deltaTime;
		}
		else if (Input.GetMouseButtonUp(1))
		{
			Vector3 dir = GetDirection();
			GetBullet(dir).StartMove(dir * 5);
			fireTimer = 0f;
		}
	}

    private Vector3 GetDirection()
	{
		Vector3 mousePos = ingameCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -ingameCam.transform.position.z));
		return (mousePos - transform.position).normalized;
	}

    private IProjectile GetBullet(Vector3 dir)
	{
        GameObject clone = null;
		if (InventoryManager.Instance.inventoryData.isGetBigGun)
		{
			clone = fireTimer < 1f ?
					ObjectPoolManager.Instance.GetObject(bullet, transform.position, transform.rotation) :
					ObjectPoolManager.Instance.GetObject(chargedBullet, transform.position, transform.rotation);
		}
        else
		{
            clone = ObjectPoolManager.Instance.GetObject(bullet, transform.position + dir * .75f, transform.rotation);
		}
        clone.tag = "PlayerWeapon";

		return clone.GetComponent<IProjectile>();
	}

	public void GroundCheck()
    {
        if (rigid.velocity.y > 0 || isCanJump) return;

        var hit = Physics2D.BoxCast(transform.position + (Vector3.down * 0.05f), new Vector2(0.5f,1f), 0, Vector2.down, 0.1f, groundLayerMask);
        if (hit.collider != null)
        {
            if(!isCanJump)
			{
                Sound.SoundManager.Instance.PlayEFF("Land");
			}
            isCanJump = true;
        }
    }

    private IEnumerator OnDie()
    {
        isCanMove = false;
		deathParticle.gameObject.SetActive(true);
		deathParticle.transform.SetParent(null);
        sprite.color = new Color(1, 1, 1, 0);
        eye.color = sprite.color;
        yield return new WaitForSeconds(0.5f);
        MapMoveManager.Instance.MoveScene(MapMoveManager.MoveType.Middle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.transform.CompareTag("CanTakeDmg") || collision.transform.CompareTag("EnemyWeapon")) && !isDie)
        {
            isDie = true;
            StartCoroutine(OnDie());
        }
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if ((collision.transform.CompareTag("CanTakeDmg") || collision.transform.CompareTag("EnemyWeapon")) && !isDie)
		{
			isDie = true;
			StartCoroutine(OnDie());
		}
	}
}
