using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ
{
	public class EnterTrigger : MonoBehaviour 
	{
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
			if (collider.tag == "Player" && !audioSource.isPlaying)
			{
				audioSource.enabled = false;
				audioSource.enabled = true;
			}
		}

		#endregion
	}
}