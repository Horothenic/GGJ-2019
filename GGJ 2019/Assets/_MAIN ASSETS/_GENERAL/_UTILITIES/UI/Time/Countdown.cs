using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.UI
{
	public class Countdown : MonoBehaviour 
	{
		// //////////////////////// //
		// ////// ATTRIBUTES ////// //
		// //////////////////////// //

		[Header ("TWEAKS")]
		[SerializeField] private float startTime = 3;
		[SerializeField] private float delay = 1;
		[SerializeField] private float currentCount;
		[SerializeField] private Transform pivot;
		[SerializeField] private AudioClip beep;

		[Header ("UI")]
		[SerializeField] private Text countTextPrefab;

		private Text currentCountText;
		private Coroutine coroutine;

		// //////////////////// //
		// ////// EVENTS ////// //
		// //////////////////// //

		public delegate void VoidDelegate ();
		public VoidDelegate CountdownEnded;

		// //////////////////////// //
		// ////// BEHAVIOURS ////// //
		// //////////////////////// //

		public void StartCountdown ()
		{
			ResetCountdown ();
		}

		private IEnumerator Countdown_Coroutine ()
		{
			while (currentCount > 0)
			{
				currentCountText = Instantiate (countTextPrefab, pivot.position, pivot.rotation, pivot);
				currentCountText.text = currentCount.ToString ();
				currentCountText.gameObject.transform.localEulerAngles = Vector3.zero;

				if (beep != null)
				{
					AudioManager.ReproduceSound (beep);
				}

				yield return new WaitForSecondsRealtime (delay);

				currentCount--;

				//Destroy (currentCountText.gameObject);
			}

			EndCountdown ();
		}

		public void ResetCountdown ()
		{
			currentCount = startTime;

			if (coroutine != null)
				StopCoroutine (coroutine);

			coroutine = StartCoroutine (Countdown_Coroutine ());
		}

		private void EndCountdown ()
		{
			currentCountText = Instantiate (countTextPrefab, pivot.position, Quaternion.identity, pivot);
			currentCountText.text = "GO";
			currentCountText.gameObject.transform.localEulerAngles = Vector3.zero;

			if (beep != null)
			{
				AudioManager.ReproduceSound (beep);
			}

			//Invoke ("DestroyLastCount", 0.5f);

			if (CountdownEnded != null)
			{
				CountdownEnded ();
				CountdownEnded = null;
			}
		}

		private void DestroyLastCount ()
		{
			Destroy (currentCountText.gameObject);
		}
	}
}