using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Utilities.GameObjects		
{
	public class Resize : MonoBehaviour 
	{
		// //////////////////////// //
		// ////// ATTRIBUTES ////// //
		// //////////////////////// //

		[Header ("TWEAKS")]
		[SerializeField] private float incrementFactor = 2f; 
		[SerializeField] private float transitionTime = 0.5f;

		private Vector3 originalScale;

		// //////////////////////// //
		// ////// BEHAVIOURS ////// //
		// //////////////////////// //

		void Start ()
		{
			originalScale = this.gameObject.transform.localScale;

			ResizeObject ();
		}

		private void ResizeObject ()
		{
			Vector3 from, to;

			from = originalScale;
			to = originalScale * incrementFactor;

			Action <object> onUpdateAction = (newValue => 
			{
				this.gameObject.transform.localScale = (Vector3) newValue;
			});

			Action <object> onCompleteAction = (newValue => 
			{
				Destroy (this.gameObject);
			});

			Hashtable hash = new Hashtable ();
			hash.Add ("from", from);
			hash.Add ("to", to);
			hash.Add ("time", transitionTime);
			hash.Add ("easetype", iTween.EaseType.linear);
			hash.Add ("ignoretimescale", true);
			hash.Add ("onupdate", onUpdateAction);
			hash.Add ("oncomplete", onCompleteAction);

			iTween.ValueTo (this.gameObject, hash);
		}
	}
}