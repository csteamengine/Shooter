using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{

	public Transform Player;
	public int MoveSpeed;
	public int MaxDist;
	public int MinDist;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		transform.LookAt(Player);

		if (Vector3.Distance(transform.position, Player.position) >= MinDist)
		{

			transform.position += transform.forward * MoveSpeed * Time.deltaTime;


			if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
			{
				//Here Call any function U want Like Shoot at here or something
			}

		}
	}
}