using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Utitilities for UI namespace.
/// </summary>
namespace Utilities.UI
{
	/// <summary>
	/// Opens an URL when button pressed, URL is assigned on Editor.
	/// </summary>
	[RequireComponent (typeof (Button))] 
	public class OpenURLWithButton : MonoBehaviour 
	{
		// //////////////////////// //
		// ////// ATTRIBUTES ////// //
		// //////////////////////// //

		// URL //

		[Header ("URL")]
		/// <summary> The URL. </summary>
		[SerializeField] private string url;

		// //////////////////////// //
		// ////// BEHAVIOURS ////// //
		// //////////////////////// //

		/// <summary> Start this instance. </summary>
		void Start () 
		{
			this.gameObject.GetComponent <Button> ().onClick.AddListener (OpenURL);
		}

		/// <summary> Opens the URL. </summary>
		private void OpenURL ()
		{
			Application.OpenURL (url);
		}
	}
}