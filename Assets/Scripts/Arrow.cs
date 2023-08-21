using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
	Rigidbody2D _rb;
	Transform _targetTr;
	private void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
		_targetTr = FindObjectOfType<Target>().transform;
	}

	private void Update()
	{
		//float angle = Quaternion.LookRotation(_rb.velocity).y;
		if (_rb.bodyType == RigidbodyType2D.Dynamic)
			transform.rotation = Quaternion.LookRotation(_rb.velocity, Vector3.up);
	}

	private void FixedUpdate()
	{
		if (transform.position.y < -GameManager.Single.RightUpperCorner.y || transform.position.x > _targetTr.position.x + 3)
		{
			GameManager.Single.Lives--;
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Target"))
		{
			GameManager.Single.Score++;
			transform.SetParent(collision.transform);
			_rb.velocity = Vector2.zero;
			_rb.bodyType = RigidbodyType2D.Kinematic;
			Destroy(gameObject, 1);
			collision.GetComponent<Target>().SetNewPos();
		}
	}
}
