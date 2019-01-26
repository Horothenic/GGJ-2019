using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Transforms
{
	public class FollowPosition : MonoBehaviour 
	{
		#region ATTRIBUTES

		[SerializeField] private Transform followTransform;
		[SerializeField] private bool rotate = true;
		[SerializeField] private Vector3 offset;

		private Vector3 originalRotation;

		#endregion

		#region INITIALIZATION

		void Awake ()
		{
			originalRotation = transform.eulerAngles;
		}

		#endregion

		#region BEHAVIOURS

		void Update ()
		{
			transform.position = followTransform.position + offset;

			if (!rotate)
			{
				transform.eulerAngles = originalRotation;
			}
		}

		#endregion
	}
}