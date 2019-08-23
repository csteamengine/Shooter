using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
	private float distance;
	private bool grabbed = false;
	private Rigidbody rb;

	// Start is called before the first frame update
	void Start()
    {
		rb = gameObject.GetComponent<Rigidbody>();
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
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
	}

    public void Throw(Vector3 vector, float throwForce)
	{
		Release();
		rb.AddForce(vector * throwForce, ForceMode.Impulse);
	}

    public bool Grab(Camera fpsCam)
	{
		grabbed = true;
		rb.useGravity = false;
		rb.detectCollisions = true;
		transform.parent = fpsCam.transform;
		return true;
	}

    public void Release()
	{
        Vector3 velocity = rb.velocity;
		grabbed = false;
		rb.useGravity = true;
		transform.parent = null;
	}
}
