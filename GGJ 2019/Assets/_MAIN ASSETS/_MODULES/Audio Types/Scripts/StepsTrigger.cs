using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.Characters.FirstPerson;

namespace GGJ
{
	public class StepsTrigger : MonoBehaviour 
	{
		#region ATTRIBUTES

		[Header ("SOUNDS")]
		[SerializeField] private AudioClip[] customFootsteps;

		#endregion

		#region INTERNAL

		private FirstPersonController player;

		#endregion

		#region COLLISION_BEHAVIOURS

		void OnTriggerEnter(Collider collider)
		{
			if (collider.tag == "Player")
			{
				if (player == null)
					player = collider.gameObject.GetComponentInChildren<FirstPersonController>();

				player.m_CustomFootstepSounds = customFootsteps;
			}
		}

		void OnTriggerExit(Collider collider)
		{
			if (collider.tag == "Player")
			{
				if (player == null)
					player = collider.gameObject.GetComponentInChildren<FirstPersonController>();

				if (player.m_CustomFootstepSounds == customFootsteps)
					player.m_CustomFootstepSounds = new AudioClip[]{};
			}
		}

		#endregion
	}
}