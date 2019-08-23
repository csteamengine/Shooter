using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
	public float grabDistance;
	public Camera fpsCam;
	public float throwForce;

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
			UpdatePosition();
			DetectRelease();
			DetectThrow();
		}
	}

	void OnMouseDown()
	{
		RaycastHit hit;
		if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, grabDistance))
		{
			Grab();
		}
	}

    void OnMouseUp()
	{
		if (grabbed)
		{
			Release();
		}
	}

	public void UpdatePosition()
	{
		distance = Vector3.Distance(transform.position, fpsCam.transform.position);
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
		
	}

    public void DetectThrow()
	{
		if (Input.GetKeyDown("e"))
		{
			Release();
			rigidbody.AddForce(fpsCam.transform.forward * throwForce, ForceMode.Impulse);
		}
	}

    public void DetectRelease()
	{
		if (distance > grabDistance)
		{
			Release();
		}
	}

    public void Grab()
	{
		grabbed = true;
		rigidbody.useGravity = false;
		rigidbody.detectCollisions = true;
		transform.parent = fpsCam.transform;
	}

    public void Release()
	{
        Vector3 velocity = rigidbody.velocity;
		grabbed = false;
		rigidbody.useGravity = true;
		rigidbody.AddForce(Vector3.forward);
		transform.parent = null;
	}
}
