using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.UI
{
	public class LoadingWWW : MonoBehaviour 
	{
		private static LoadingWWW instance;

		[Header ("LOADING ELEMENTS")]
		[SerializeField] private Image loadingImage;
		[SerializeField] private Text loadingText;
		[SerializeField] private float currentValue = 0;

		void Awake ()
		{
			instance = this;
			this.gameObject.SetActive (false);
		}

		public void ResetLoading ()
		{
			instance.currentValue = 0;

			instance.UpdateLoadingUI ();
		}

		public static void SetLoadingValue (float newValue)
		{
			instance.currentValue = newValue;

			instance.UpdateLoadingUI ();
		}

		private void UpdateLoadingUI ()
		{
			loadingImage.fillAmount = currentValue;
			loadingText.text = ((int)(currentValue * 100)).ToString ();
		}
	}
}