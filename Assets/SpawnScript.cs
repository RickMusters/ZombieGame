using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {

	void Update()
	{	
		if (Input.GetKeyDown(KeyCode.T))
		{
			float number = Random.value;
			string zombie = "";
			if (number > 0.2f)
			{
				zombie = "Zombie";
			}
			else
			{
				zombie = "Boomer";
			}
			
			GameObject newZombie = Instantiate(Resources.Load(zombie), Vector3.zero, Quaternion.identity) as GameObject;
			newZombie.name = "Enemy";
			newZombie.transform.Rotate(0, Random.value * 360, 0);
			newZombie.transform.Translate(0, 0, 50);
		}
	}
}
