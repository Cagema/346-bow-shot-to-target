using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] Vector2 _minMaxPos;

	private void Start()
	{
		RandomPos();
	}
	private void RandomPos()
    {
        transform.position = new Vector3(Random.Range(0, _minMaxPos.x), Random.Range(-_minMaxPos.y, _minMaxPos.y), 0);
    }

	public void SetNewPos()
	{
		Invoke(nameof(RandomPos), 1);
	}
}
