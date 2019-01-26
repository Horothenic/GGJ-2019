using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Cameras
{
	public class FieldOfViewFixer : MonoBehaviour 
	{
		// //////////////////////// //
		// ////// ATTRIBUTES ////// //
		// //////////////////////// //

		[Header ("POSITION")]
		[SerializeField] public AnimationCurve positionCurve;

		// //////////////////////// //
		// ////// BEHAVIOURS ////// //
		// //////////////////////// //

		public void FixFieldOfView (float fieldOfView)
		{
			print ("Field of view: " + fieldOfView);

			float fixedPosition = positionCurve.Evaluate (fieldOfView);

			print ("Fixed position: " + fixedPosition);

			this.transform.localPosition = new Vector3 (this.transform.localPosition.x, this.transform.localPosition.y, fixedPosition);
		}
	}
}