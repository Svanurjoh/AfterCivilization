using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class CameraController : MonoBehaviour
{
	public Transform player;

	public Vector3 offsetPos;

	private void Update()
	{
		Refresh ();
	}

	public void Refresh()
	{
		Assert.IsNotNull (player);
		transform.position = player.TransformPoint (offsetPos);
		transform.LookAt (player);
	}
}
