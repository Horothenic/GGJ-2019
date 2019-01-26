using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Transforms
{
	public class BlockLocalRotation : MonoBehaviour 
	{
		#region ATTRIBUTES

		Vector3 originalEulerAngles;

		#endregion

		#region INITIALIZATION

		void Start ()
		{
			originalEulerAngles = transform.localEulerAngles;
		}

		#endregion

		#region BEHAVIOURS

		void Update ()
		{
			transform.localEulerAngles = originalEulerAngles;
		}

		#endregion
	}
}