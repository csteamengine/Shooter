using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
	Vector3 objectPos;
	float distance;

	public bool canHold = true;
	public float throwForce;
	public GameObject item;
	public GameObject tempParent;
	public float grabDistance;
	public bool isHolding = false;

	private Rigidbody rigidBody;

    void Start()
	{
		rigidBody = item.GetComponent<Rigidbody>();
	}

	void Update()
	{
		if (isHolding)
		{
			distance = Vector3.Distance(item.transform.position, tempParent.transform.position);
			if (distance >= grabDistance)
			{
				isHolding = false;
			}

			rigidBody.velocity = Vector3.zero;
			rigidBody.angularVelocity = Vector3.zero;
			item.transform.SetParent(tempParent.transform);

			if (Input.GetKeyDown("e"))
			{
				Throw();
			}
		}
		else
		{
			Release();
		}
	}

    void OnMouseDown()
	{
		if(distance <= grabDistance)
		{
			Grab();
		}
	}

    void OnMouseUp()
	{
		isHolding = false;
	}

    void Release()
	{
		objectPos = item.transform.position;
		item.transform.SetParent(null);
		rigidBody.useGravity = true;
		item.transform.position = objectPos;
	}

    void Grab()
	{
		isHolding = true;
		rigidBody.useGravity = false;
		rigidBody.detectCollisions = true;
	}

	void Throw()
	{
		rigidBody.AddForce(tempParent.transform.forward * throwForce, ForceMode.Impulse);
		isHolding = false;
	}
}
