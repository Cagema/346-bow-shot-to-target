using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Bow : MonoBehaviour
{
	[SerializeField] GameObject _arrowPrefab;
	[SerializeField] Transform _arrowDir;
	[SerializeField] Transform _arrow;
    [SerializeField] Vector2 _rotMinMax;
	[SerializeField] float _midAngle;
	[SerializeField] float _nockSpeed;
	[SerializeField] float _forceMult;
	[SerializeField] float _forceMax;
	[SerializeField] float _force;

	float _timeSpend = 1;
	bool _nock = false;
	Vector3 _arrowStartPos;

	private void Start()
	{
		_arrowStartPos = _arrow.localPosition;
	}
	private void Update()
	{
		if (GameManager.Single.GameActive)
		{
			_timeSpend += Time.deltaTime;
			float angle = Mathf.Cos(_timeSpend) * (_rotMinMax.x - _midAngle);
			transform.rotation = Quaternion.Euler(0, 0, _midAngle + angle);

			if (Input.GetMouseButtonDown(0))
			{
				_nock = true;
			}

			if (Input.GetMouseButton(0) && _nock)
			{
				if (_force < _forceMax)
				{
					_force += Time.deltaTime * _nockSpeed;
					_arrow.localPosition = new(_arrow.localPosition.x - _force * 0.3f * Time.deltaTime, _arrow.localPosition.y, 0);
				}
			}

			if (Input.GetMouseButtonUp(0) && _nock)
			{
				Shot();
				_nock = false;
				_force = 0;
				_arrow.localPosition = _arrowStartPos;
			}
		}

		
	}

	private void Shot()
	{
		var newGO = Instantiate(_arrowPrefab, transform.position, transform.rotation);
		newGO.GetComponent<Rigidbody2D>().AddForce(_force * _forceMult * (Vector2)(_arrowDir.position - transform.position), ForceMode2D.Impulse);
		Debug.Log(_forceMult * _force);
	}
}
