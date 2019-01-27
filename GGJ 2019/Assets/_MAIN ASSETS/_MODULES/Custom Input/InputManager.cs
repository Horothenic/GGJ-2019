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
		[SerializeField] private float longPressThreshold = 3f;

		[Header("AUDIO")]
		[SerializeField] private AudioClip doubleTapSound;
		[SerializeField] private AudioClip longPressSound;

		private float doubleTapTime = -1;
		private float longPressTime;

		private bool longPressTriggered = false;

		public static BasicDelegates.VoidDelegate DoubleTapEvent;
		public static BasicDelegates.VoidDelegate LongPressEvent;

		private int taps = 0;

		#endregion

		#region INTERNAL

		private AudioSource audioSource;

		#endregion

		#region INITIALIZATION

		void Awake()
		{
			audioSource = GetComponentInChildren<AudioSource>();
		}

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
				if (doubleTapTime > 0 && Time.time <= doubleTapTime)
				{
					taps ++;

					if (taps == 2)
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

			audioSource.clip = longPressSound;
			audioSource.PlayOneShot(audioSource.clip);

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