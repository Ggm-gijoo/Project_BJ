using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum SwitchType
{
	OnOff,
	OnlyOne
}

[RequireComponent(typeof(Rigidbody2D))]
public class SwitchButton : MonoBehaviour
{
	public SwitchType switchType;

	public SpriteRenderer spriteRenderer;
	public Sprite off;
	public Sprite on;
	public Sprite noneCheck;
	public Sprite check;

	private bool isOn;

	public UnityEvent onEvent;
	public UnityEvent offEvent;

	private void Start()
	{
		InitSprite();
	}

	private void InitSprite()
	{
		switch (switchType)
		{
			case SwitchType.OnOff:
				spriteRenderer.sprite = off;
				break;
			case SwitchType.OnlyOne:
				spriteRenderer.sprite = noneCheck;
				break;
			default:
				break;
		}
	}
	private void ChangeSprite()
	{
		switch (switchType)
		{
			case SwitchType.OnOff:
				spriteRenderer.sprite = on;
				break;
			case SwitchType.OnlyOne:
				spriteRenderer.sprite = check;
				break;
			default:
				break;
		}
	}

	public void Hit()
	{
		switch (switchType)
		{
			case SwitchType.OnOff:
				if(isOn)
				{
					isOn = false;
					InitSprite();
					offEvent?.Invoke();
				}
				else
				{
					isOn = true;
					ChangeSprite();
					onEvent?.Invoke();
				}
				break;
			case SwitchType.OnlyOne:
				isOn = true;
				ChangeSprite();
				onEvent?.Invoke();
				break;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("CanTakeDmg") || collision.CompareTag("PlayerWeapon") || collision.CompareTag("EnemyWeapon") || collision.CompareTag("CanDestroy"))
		{
			Hit();
		}
	}


}
