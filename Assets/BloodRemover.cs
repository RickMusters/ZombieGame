using UnityEngine;
using System.Collections;

public class BloodRemover : MonoBehaviour {
	void Start () {
		Destroy(gameObject, 1);
	}
}
