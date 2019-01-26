using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Utilities.UI
{
	[RequireComponent (typeof (Text))]
	public class Timer : MonoBehaviour 
	{
		// //////////////////////// //
		// ////// ATTRIBUTES ////// //
		// //////////////////////// //

		[Header ("TWEAKS")]
		[SerializeField] private float startTime = 10;
		[SerializeField] private float endTime = 0;
		[SerializeField] private float currentTime;

		[Header ("UI")]
		[SerializeField] private Text timerText;

		// //////////////////// //
		// ////// EVENTS ////// //
		// //////////////////// //

		public delegate void VoidDelegate ();
		public VoidDelegate TimerEnded;

		private Coroutine coroutine;

		// //////////////////////// //
		// ////// BEHAVIOURS ////// //
		// //////////////////////// //

		public void StartTimer ()
		{
			ResetTimer ();
		}

		public void ResetTimerText ()
		{
			timerText.text = String.Format ("{0:0.00}", startTime);
		}

		private IEnumerator Timer_Coroutine ()
		{
			while (currentTime > endTime)
			{
				yield return new WaitForEndOfFrame ();

				currentTime -= UnityEngine.Time.deltaTime;

				float clampedTime = Mathf.Clamp (currentTime, endTime, startTime);
				timerText.text = String.Format ("{0:0.00}", clampedTime);
			}

			EndTimer ();
		}

		public void StopTimer ()
		{
			StopAllCoroutines ();

			print ("Timer stopped");
		}

		public void ResetTimer ()
		{
			currentTime = startTime;

			if (coroutine != null)
				StopCoroutine (coroutine);

			coroutine = StartCoroutine (Timer_Coroutine ());
		}

		private void EndTimer ()
		{
			if (TimerEnded != null)
			{
				TimerEnded ();

				TimerEnded = null;
			}
		}
	}
}