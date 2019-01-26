using System.Collections;
using UnityEngine;

namespace Helpers.GameObject
{
	public class FollowObject : MonoBehaviour 
	{
		// //////////////////////// //
		// ////// ATTRIBUTES ////// //
		// //////////////////////// //

		[Header ("TWEAKS")]
		[SerializeField] private Transform objectToFollow;
		[SerializeField] private Vector3 upVector = Vector3.up;

		// //////////////////////// //
		// ////// BEHAVIOURS ////// //
		// //////////////////////// //

		void Update ()
		{
			this.gameObject.transform.LookAt (objectToFollow, upVector);
		}
	}
}