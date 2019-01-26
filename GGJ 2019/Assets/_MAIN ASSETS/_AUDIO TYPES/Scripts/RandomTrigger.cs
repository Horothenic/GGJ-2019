using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ
{
	public class RandomTrigger : MonoBehaviour 
	{
		#region ATTRIBUTES

		[Header("TWEAKS")]
		[SerializeField] private float delay = 5f;
		[Range(1,100)] [SerializeField] private float percentage = 50f;

		#endregion

		#region INTERNAL

		private AudioSource audioSource;

		#endregion

		#region INITIALIZATION

		void Awake()
		{
			audioSource = GetComponentInChildren<AudioSource>();
		}

		void Start()
		{
			StartCoroutine(TriggerSound());
		}

		#endregion

		#region BEHAVIOURS

		private IEnumerator TriggerSound()
		{
			while (true)
			{
				yield return new WaitForSeconds(delay);

				if (Random.Range(0, 101) > percentage)
				{
					if (!audioSource.isPlaying)
					{
						audioSource.enabled = false;
						audioSource.enabled = true;
					}
				}
			}
		}

		#endregion
	}
}