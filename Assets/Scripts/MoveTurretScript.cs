using UnityEngine;
using System.Collections;

public class MoveTurretScript : MonoBehaviour {
	public float speed = 20.0f;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		RotateTurret ();
	}

	public void RotateTurret()
	{
		if (Input.GetMouseButton(1))
		{
			float pos = Input.mousePosition.y;
			//this.transform.rotation.y = Input.mousePosition.y;
			if (pos > Screen.height/2) {
				Debug.Log (pos);
				this.transform.Rotate (0.0f, speed * Time.deltaTime, 0.0f, Space.Self);
			} else if (pos < Screen.height/2) {
				Debug.Log (pos);
				this.transform.Rotate (0.0f, -(speed * Time.deltaTime), 0.0f, Space.Self);
			} else {
				Debug.Log (pos);
				//this.transform.Rotate (0.0f, speed * Time.deltaTime, 0.0f, Space.Self);
			}
		}
	}
}
