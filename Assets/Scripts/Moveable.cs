using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
	private float distance;
	private bool grabbed = false;
	private Rigidbody rigidbody;

	// Start is called before the first frame update
	void Start()
    {
		rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
	{
		if (grabbed)
		{
			UpdateVectors();
		}
	}

	public void UpdateVectors()
	{
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
	}

    public void Throw(Vector3 vector, float throwForce)
	{
		Release();
		rigidbody.AddForce(vector * throwForce, ForceMode.Impulse);
	}

    public bool Grab(Camera fpsCam)
	{
		grabbed = true;
		rigidbody.useGravity = false;
		rigidbody.detectCollisions = true;
		transform.parent = fpsCam.transform;
		return true;
	}

    public void Release()
	{
        Vector3 velocity = rigidbody.velocity;
		grabbed = false;
		rigidbody.useGravity = true;
		transform.parent = null;
	}
}
