using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.UI.PopUps
{
	public abstract class PopUp : MonoBehaviour 
	{
		// //////////////////////// //
		// ////// ATTRIBUTES ////// //
		// //////////////////////// //

		// //////////////////////// //
		// ////// BEHAVIOURS ////// //
		// //////////////////////// //

		protected void PopUpAction (UnityAction action)
		{
			if (action != null)
				action ();

			DestroyPopUp ();
		}

		protected void DestroyPopUp ()
		{
			Destroy (this.gameObject);
		}
	}
}