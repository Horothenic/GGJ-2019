using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameObject helpers namespace.
/// </summary>
namespace Helpers.GameObject
{
	/// <summary>
	/// Rotates gameObject.
	/// </summary>
	public class Rotate : MonoBehaviour 
	{
		// //////////////////////// //
		// ////// ATTRIBUTES ////// //
		// //////////////////////// //

		/// <summary> The speed that the gameObject will rotate (used with Time.deltaTime). </summary>
		[SerializeField] private float speed = 1;

		[Header ("Axis")]
		[SerializeField] private bool x = false;
		[SerializeField] private bool y = true;
		[SerializeField] private bool z = false;

		// //////////////////////////////// //
		// ////// ROTATION BEHAVIOUR ////// //
		// //////////////////////////////// //

		/// <summary> Update this instance. </summary>
		void Update ()
		{
			float xRotation = x ? speed * Time.deltaTime : 0;
			float yRotation = y ? speed * Time.deltaTime : 0;
			float zRotation = z ? speed * Time.deltaTime : 0;

			transform.Rotate (xRotation, yRotation, zRotation, Space.World);
		}
	}
}