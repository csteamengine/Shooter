using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public float grabDistance;
    public Camera fpsCam;
    public float throwForce;

    private bool holdingObject;
    private MoveableObject target;


    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (holdingObject)
        {
            DetectThrow();
        }
        if (Input.GetKeyDown("e"))
        {
            AttemptGrab();
        }
        if (Input.GetKeyUp("e"))
        {
            AttemptRelease();
        }
    }

    void AttemptGrab()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, grabDistance))
        {
            target = hit.transform.GetComponent<MoveableObject>();
            if (target != null)
            {
                holdingObject = target.Grab(fpsCam);
            }
        }
    }

    void AttemptRelease()
    {
        if(holdingObject && target != null)
        {
            target.Release();
        }
    }

    void DetectThrow()
    {
        if (Input.GetMouseButtonDown(0) && target != null)
        {
            target.Throw(fpsCam.transform.forward, throwForce);
        }
    }

}
