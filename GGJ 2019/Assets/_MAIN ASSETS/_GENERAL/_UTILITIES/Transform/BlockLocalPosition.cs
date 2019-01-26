using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Transforms
{
	public class BlockLocalPosition : MonoBehaviour 
	{
		#region ATTRIBUTES

		[Header ("AXIS")]
		[SerializeField] private Block_Axis axis = Block_Axis.X;

		Vector3 originalLocalPosition;

		#endregion

		#region INITIALIZATION

		void Start ()
		{
			originalLocalPosition = transform.localPosition;
		}

		public void Reset ()
		{
			originalLocalPosition = transform.localPosition;
		}

		#endregion

		#region BEHAVIOURS

		void Update ()
		{
			switch (axis)
			{
				case Block_Axis.X: transform.localPosition = new Vector3 (originalLocalPosition.x, transform.localPosition.y, transform.localPosition.z); break;
				case Block_Axis.Y: transform.localPosition = new Vector3 (transform.localPosition.x, originalLocalPosition.y, transform.localPosition.z); break;
				case Block_Axis.Z: transform.localPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y, originalLocalPosition.z); break;
			}
		}

		#endregion
	}
}