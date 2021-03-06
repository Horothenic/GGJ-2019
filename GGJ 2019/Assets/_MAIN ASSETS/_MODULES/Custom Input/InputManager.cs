﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utilities.Inspector;

namespace CustomInputs
{
	public class InputManager : MonoBehaviour 
	{
		#region ATTRIBUTES

		[Header("TWEAKS")]
		[SerializeField] private float doubleTapThreshold = 0.2f;
		[SerializeField] private float longPressThreshold = 1.5f;

		[Header("AUDIO")]
		[SerializeField] private AudioSource audioSource;
		[SerializeField] private AudioClip doubleTapSound;

		private float doubleTapTime = -1;
		private float longPressTime;

		private bool longPressTriggered = false;

		public static BasicDelegates.VoidDelegate DoubleTapEvent;
		public static BasicDelegates.VoidDelegate LongPressEvent;

		private int taps = 0;

		#endregion

		#region UPDATES

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				longPressTime = Time.time + longPressThreshold;
			}

			if (!longPressTriggered && Input.GetKey(KeyCode.Space))
			{
				if (Time.time >= longPressTime)
				{
					LongPress();
				}
			}

			if (Input.GetKeyUp(KeyCode.Space))
			{
				taps ++;

				if (doubleTapTime > 0 && Time.time <= doubleTapTime)
				{
					if (taps >= 2)
					{
						DoubleTap();
					}
				}

				doubleTapTime = Time.time + doubleTapThreshold;

				longPressTriggered = false;
			}
		}

		#endregion

		#region BEHAVIOURS

		private void LongPress()
		{
			longPressTriggered = true;
			Debug.Log ("Long press");

			if (LongPressEvent != null)
				LongPressEvent();
		}

		private void DoubleTap()
		{
			taps = 0;
			doubleTapTime = -1;
			Debug.Log ("Double tap");

			audioSource.clip = doubleTapSound;
			audioSource.PlayOneShot(audioSource.clip);

			if (DoubleTapEvent != null)
				DoubleTapEvent();
		}

		#endregion
	}
}