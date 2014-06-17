using UnityEngine;
using System.Collections;

public class ZombieScript : MonoBehaviour {
	public int hitpoints;
	bool attacking;
	float number = Random.value;
	
	
	void Start()
	{
		transform.position = new Vector3(transform.position.x,0,transform.position.z);	
		gameObject.name = "Enemy";
	}
	void FixedUpdate()
	{
		rigidbody.velocity = Vector3.zero;
		GameObject player = GameObject.Find("Player");
		transform.LookAt(player.transform.position);
		
		Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
				RaycastHit hit;
				Debug.DrawRay(ray.origin, ray.direction * 200);
				if(Physics.Raycast(ray, out hit, 200))
				{
					if (hit.collider.gameObject.name == "Player")
					{
						attacking = true;
					}
					else
					{
						attacking = false;
						
					}
				}	
		
		
		/*if(Vector3.Distance(transform.position,player.transform.position) < 100)
		{
				attacking = true;
		}*/
		
		if(attacking)
		{
			rigidbody.AddRelativeForce(0, 0, 250);
		}
	}
	
	public void TakeDamage(int damage)
	{
		hitpoints -= damage;
		
		if (hitpoints <= 0)
		{
			Instantiate(Resources.Load("Zombie Blood"), transform.position, Quaternion.identity);
			Destroy(gameObject);
			
			if( number <0.1f)
			{
				Instantiate(Resources.Load("Health Pack"), transform.position, Quaternion.identity);
			}
			if(number <0.2f)
			{
				Instantiate(Resources.Load("Ammo"), transform.position, Quaternion.identity);	
			}
		}
	}
	
	void OnCollisionStay(Collision col)
	{
		if (col.collider.gameObject.name == "Player")
		{
			col.collider.gameObject.GetComponent<PlayerScript>().TakeDamage(1);
		}
	}
}
