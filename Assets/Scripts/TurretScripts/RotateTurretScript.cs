using UnityEngine;
using System.Collections;

public class RotateTurretScript : MonoBehaviour
{
	public float speed = 0.001f;
	private bool isSelected = false;

	void Update()
	{
		if (!isSelected) 
		{
			if (Input.GetMouseButtonDown (0)) 
			{
				CheckForTurret ();
			}
		}
		else 
		{
			if (Input.GetMouseButton (0)) 
			{
				RotateTurret ();
			}
			isSelected = false;
		}
	}
	private void CheckForTurret()
	{
		RaycastHit hit;
		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 30.0f)) 
		{
			if (hit.collider.tag == "Turret") 
			{
				isSelected = true;
			}
		}
	}
	private void RotateTurret()
	{
		float rotY = Input.mousePosition.x;
		Debug.Log (Input.mousePosition.x);
		this.gameObject.transform.Rotate (0, rotY*speed, 0);
	}
}
