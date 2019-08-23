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

	// Start is called before the first frame update
	void Start()
    {

    }

    void Update()
	{
		if (grabbed)
		{
			distance = Vector3.Distance(transform.position, fpsCam.transform.position);
            if (distance > grabDistance)
			{
				Release();
			}
            if (Input.GetKeyDown("e"))
			{
				Release();
				gameObject.GetComponent<Rigidbody>().AddForce(fpsCam.transform.forward * throwForce, ForceMode.Impulse);
			}
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

    public void Grab()
	{
		grabbed = true;
		gameObject.GetComponent<Rigidbody>().useGravity = false;
		gameObject.GetComponent<Rigidbody>().isKinematic = true;
		gameObject.GetComponent<Rigidbody>().detectCollisions = true;
		transform.parent = fpsCam.transform;
	}

    public void Release()
	{
        Vector3 velocity = gameObject.GetComponent<Rigidbody>().velocity;
		grabbed = false;
		gameObject.GetComponent<Rigidbody>().useGravity = true;
		gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward);
		gameObject.GetComponent<Rigidbody>().isKinematic = false;
		transform.parent = null;
	}
}
