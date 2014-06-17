using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	int ammo = 30;
	bool haveAmmo = true;
	//Vector2 lastMousePos
	//Vector2 currentMousePos;
	
	public Camera cam1;
	public Camera cam2;
	
	//public float hitpoints;
	public GameObject swordRange;

	public AudioClip swordMiss;
	public AudioClip swordHit;
	public AudioClip shot;
	
	GUIText playerAmmoDisplay;
	GUIText playerHitpointsDisplay;
	
	void Start()
	{
		PlayerHealth.maxHealth = 100;
		//currentMousePos = Input.mousePosition;
		//lastMousePos = Input.mousePosition;
		playerAmmoDisplay = GameObject.Find("Player Ammo UI").GetComponent<GUIText>();
	}
	
	void Update()
	{
		GameObject.Find("Camera Follower").transform.position = transform.position;
		;
		
		playerAmmoDisplay.text = ammo.ToString();
	}
	
	void FixedUpdate()
	{
		
		if(cam2.enabled)
		{	
			//lastMousePos = currentMousePos;
			//currentMousePos = Input.mousePosition;
			//Vector2 mousePosDelta = lastMousePos - currentMousePos;
			//mousePosDelta *= 0.6f;
			//transform.Rotate(0, -mousePosDelta.x, 0);
			transform.Rotate(0, Input.GetAxis("Horizontal")*0.4f,0);
		}
		else if(cam1.enabled)
		{
			Ray mouseRay = cam1.ScreenPointToRay(Input.mousePosition);
			//RaycastHit mouseRaycastHit;
		
			RaycastHit[] raycastHits = Physics.RaycastAll(mouseRay);
		
			foreach(RaycastHit h in raycastHits)
			{
				if(h.collider.name == "Ground")
				{
					Vector3 pos = h.point;
					pos.y=0;
					transform.LookAt(pos);
				}
			}
		}
		
		if(Input.GetKeyDown(KeyCode.C))
		{
			if(cam1.enabled)
			{
				cam1.enabled = false;
				cam2.enabled = true;
				Screen.lockCursor = true;
			}
			else
			{
				cam1.enabled = true;
				cam2.enabled = false;
				Screen.lockCursor = false;
			}
		}
		
		
	
		
		rigidbody.velocity = Vector3.zero;
		if (Input.GetKey(KeyCode.W))
		{
			rigidbody.AddRelativeForce(new Vector3(0, 0, 1000));
		}
		else if (Input.GetKey(KeyCode.S))
		{
			rigidbody.AddRelativeForce(new Vector3(0, 0, -500));	
		}
		
		/*if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate(0, -10, 0);	
		}
		else if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(0, 10, 0);
		}*/
		
		if (Input.GetKeyDown(KeyCode.Space))
		{
			bool hasHitZombie = false;
			Collider[] collisions = Physics.OverlapSphere(swordRange.transform.position, 1);
			foreach (Collider col in collisions)
			{
				if (col.name == "Enemy")
				{
					hasHitZombie = true;
					col.gameObject.GetComponent<ZombieScript>().TakeDamage(10);
					//Instantiate(Resources.Load("Zombie Blood"), col.transform.position, Quaternion.identity);
					//Destroy(col.gameObject);
				}
			}
			if (hasHitZombie)
			{
				AudioSource.PlayClipAtPoint(swordHit, swordRange.transform.position);	
			}
			else
			{
				AudioSource.PlayClipAtPoint(swordMiss, swordRange.transform.position);	
			}
		}
		
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			if(haveAmmo)
			{
				AudioSource.PlayClipAtPoint(shot, transform.position);
				Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
				RaycastHit hit;
				Debug.DrawRay(ray.origin, ray.direction * 200);
				if(Physics.Raycast(ray, out hit, 200))
				{
					if (hit.collider.gameObject.name == "Enemy")
					{
						 hit.collider.gameObject.GetComponent<ZombieScript>().TakeDamage(5);
						//Instantiate(Resources.Load("Zombie Blood"), hit.collider.transform.position, Quaternion.identity);
						//Destroy(hit.collider.gameObject);
					}
				}
			}
		}
	}
	
	public void TakeDamage(int damage)
	{
		PlayerHealth.maxHealth -= damage;
		if(PlayerHealth.maxHealth <0)
			Application.LoadLevel(0);
		
	}
	void OnTriggerEnter(Collider col)
	{
		if(col.collider.gameObject.name == "Health Pack(Clone)")
		{
			PlayerHealth.maxHealth +=40;
			if(PlayerHealth.maxHealth > 100)
			{
				PlayerHealth.maxHealth = 100;	
			}
			Destroy(col.collider.gameObject);
		}
		
		if(col.collider.gameObject.name == "Ammo(Clone")
		{
			ammo += 10;
			if(ammo > 30)
			{
				ammo = 30;	
			}
			Destroy(col.collider.gameObject);
		}
			
	}
	

			
		
}
