using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Transforms
{
	public class BlockRotation : MonoBehaviour 
	{
		#region ATTRIBUTES

		[Header ("AXIS")]
		[SerializeField] private Block_Axis axis = Block_Axis.X;

		[Header ("TWEAKS")]
		[SerializeField] private bool blockOnZeroAll = false;

		Vector3 originalRotation;

		#endregion

		#region INITIALIZATION

		void Start ()
		{
			originalRotation = transform.eulerAngles;
		}

		#endregion

		#region BEHAVIOURS

		void Update ()
		{
			if (blockOnZeroAll)
			{
				transform.eulerAngles = Vector3.zero;
			}
			else
			{
				switch (axis)
				{
					case Block_Axis.X: transform.eulerAngles = new Vector3 (originalRotation.x, transform.eulerAngles.y, transform.eulerAngles.z); break;
					case Block_Axis.Y: transform.eulerAngles = new Vector3 (transform.eulerAngles.x, originalRotation.y, transform.eulerAngles.z); break;
					case Block_Axis.Z: transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, originalRotation.z); break;
				}
			}
		}

		#endregion
	}

	public enum Block_Axis
	{
		X,
		Y,
		Z
	}
}