using UnityEngine;
using System.Collections;

public class SeekerMissle : MonoBehaviour {
	
	bool follow = false; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
				RaycastHit hit;
				Debug.DrawRay(ray.origin, ray.direction * 200);
				if(Physics.Raycast(ray, out hit, 200))
				{
					if (hit.collider.gameObject.name == "Player")
					{
						follow = true;
					}
				}	
		
		if(follow)
		{
			rigidbody.AddRelativeForce(0, 0, 250);
		}
	
	}
}
