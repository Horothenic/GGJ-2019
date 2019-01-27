using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ
{
	public class EnterTrigger : MonoBehaviour 
	{
		#region ATTRIBUTES

		[Header("TWEAKS")]
		[SerializeField] private bool enterEnabled = false;
		[SerializeField] private bool exitEnabled = false;

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

		#region COLLISION_BEHAVIOURS

		void OnTriggerEnter(Collider collider)
		{
			if (enterEnabled && collider.tag == "Player" && !audioSource.isPlaying)
			{
				audioSource.enabled = false;
				audioSource.enabled = true;
			}
		}

		void OnTriggerExit(Collider collider)
		{
			if (exitEnabled && collider.tag == "Player" && !audioSource.isPlaying)
			{
				audioSource.enabled = false;
				audioSource.enabled = true;
			}
		}

		#endregion
	}
}