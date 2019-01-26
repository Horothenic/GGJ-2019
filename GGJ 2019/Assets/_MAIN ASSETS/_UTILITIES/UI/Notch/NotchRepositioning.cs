using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
	public class NotchRepositioning : MonoBehaviour 
	{
		#region ATTRIBUTES

		[Header ("REFERENCE")]
		[SerializeField] private Camera referenceCamera;

		#endregion

		#region INITIALIZATION

		void Start ()
		{
			AdjustBasedOnNotch ();
		}

		#endregion

		#region BEHAVIOURS

		private void AdjustBasedOnNotch ()
		{
			Vector3 topScreenPosition = referenceCamera.ScreenToWorldPoint (Vector3.up * Screen.height);
			Vector3 topSafeAreaPosition = referenceCamera.ScreenToWorldPoint (Vector3.up * Screen.safeArea.height);

			float amount = topScreenPosition.y - topSafeAreaPosition.y;

			Debug.Log ("NOTCH - " + "\"" + gameObject.name + "\"" + " repositioned " + amount + " units.");

			transform.position += Vector3.down * amount;
		}

		#endregion
	}
}