using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Utilities.UI.Scenes
{
	[RequireComponent (typeof (Button))]
	public class RestartSceneButton : MonoBehaviour 
	{
		private void Start ()
		{
			GetComponent <Button> ().onClick.AddListener (Restart);
		}

		private void Restart ()
		{
			SceneManager.LoadScene (0);
		}
	}
}