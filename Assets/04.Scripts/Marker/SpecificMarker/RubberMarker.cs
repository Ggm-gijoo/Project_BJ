using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effect;

namespace Marker
{
    public class RubberMarker : BaseMarker, IHitFromBullet
    {
		private Rigidbody2D rigidBody;
		[SerializeField] private Collider2D collider;
        [SerializeField] private float multiply = 1f;
        private float maxBounceForce = 500f; // 최대 튕김 힘
		public override string Key
		{
			get
			{
				return "RubberMarker";
			}
		}

		private void Start()
		{
			rigidBody = GetComponent<Rigidbody2D>();
		}

		private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.CompareTag("EnemyWeapon") || collision.gameObject.CompareTag("PlayerWeapon"))
            {
                collision.gameObject.tag = "CanTakeDmg";
			}
            Bounce(collision);
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.CompareTag("EnemyWeapon") || collision.gameObject.CompareTag("PlayerWeapon"))
			{
				collision.gameObject.tag = "CanTakeDmg";
			}
			Bounce(collision);
		}

		private void OnCollisionStay2D(Collision2D collision)
        {
            //Bounce(collision);
        }

        private void Bounce(Collision2D collision)
        {
            Rigidbody2D otherRb = collision.rigidbody;

            if (otherRb != null)
            {
                float otherMass = otherRb.mass;
                
                // 충돌한 물체의 속도 벡터를 구합니다.
                Vector2 collisionVelocity = collision.relativeVelocity;

                // 충돌한 물체의 노말 벡터를 구합니다. (충돌한 지점의 표면 방향을 나타냅니다)
                Vector2 collisionNormal = collision.contacts[0].normal;

                // 튕기는 힘의 크기를 계산합니다. (여기에서는 충돌 속도의 반대 방향으로 힘을 가합니다)
                float bounceForce = collisionVelocity.magnitude * otherMass * multiply;
                bounceForce = Mathf.Clamp(bounceForce, 0f, maxBounceForce);

                // 튕기는 힘의 방향을 계산합니다.
                Vector2 bounceDirection = -collisionNormal;
                
                // 튕기는 힘을 rigidbody에 적용합니다.
                otherRb.AddForce(bounceDirection * bounceForce, ForceMode2D.Impulse);
                //otherRb.AddForceAtPosition(bounceDirection * bounceForce, collisionPoint, ForceMode2D.Impulse);
                Debug.Log($"튕겨냄 {collisionVelocity.magnitude}");
            }
        }

		private void Bounce(Collider2D collision)
		{
			Rigidbody2D otherRb = collision.gameObject.GetComponent<Rigidbody2D>();
			if (otherRb != null)
			{
				float otherMass = otherRb.mass;

				// 충돌한 물체의 속도 벡터를 구합니다.
				Vector2 collisionVelocity = otherRb.velocity;

				var collisionPoint = collider.ClosestPoint(collision.transform.position);
				var collisionNormal = (collisionPoint - (Vector2)collision.transform.position ).normalized;

				// 튕기는 힘의 크기를 계산합니다. (여기에서는 충돌 속도의 반대 방향으로 힘을 가합니다)
				float bounceForce = collisionVelocity.magnitude * otherMass * multiply;
				bounceForce = Mathf.Clamp(bounceForce, 0f, maxBounceForce);

				// 튕기는 힘의 방향을 계산합니다.
				Vector2 bounceDirection = -collisionNormal;

				// 튕기는 힘을 rigidbody에 적용합니다.
				//otherRb.AddForce(bounceDirection * bounceForce, ForceMode2D.Impulse);
				otherRb.velocity = bounceDirection * collisionVelocity.magnitude * multiply;
				//otherRb.AddForceAtPosition(bounceDirection * bounceForce, collisionPoint, ForceMode2D.Impulse);
				Debug.Log($"튕겨냄 {collisionVelocity.magnitude}");
			}
		}

		public new void Hit(int damage, IProjectile projectile)
		{
			hp -= damage;
			//projectile.CollitionImplement();
			if (hp <= 0)
			{
				MarkerManager.Instance.drawMarker.AddGauge(markerType, gauge);
				EffectManager.Instance.SetEffectDefault("DestroyMarkerEffect", projectile.Position, Quaternion.identity);
				OnTriggerEnter2D(projectile.GameObject.GetComponent<Collider2D>());
				PoolThisObject();
			}
			else
			{
				StartCoroutine(HitEffect());
			}

		}

	}
}