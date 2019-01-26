using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Utilities.Transitions;

namespace Screens
{
	public class IntroScreen : MonoBehaviour 
	{
		#region ATTRIBUTES

		[Header("SEQUENCE")]
		[SerializeField] private string[] texts;
		[SerializeField] private AudioClip[] audioclips;

		[Header("UI")]
		[SerializeField] private CanvasGroup canvasGroup;
		[SerializeField] private Text text;

		[Header("AUDIO")]
		[SerializeField] private AudioSource audioSource;

		[Header("SCREEN")]
		[SerializeField] private string nextScreen = "";

		#endregion

		#region INITIALIZATION

		void Start()
		{
			StartCoroutine(Sequence());
		}

		#endregion

		#region BEHAVIOURS

		private IEnumerator Sequence()
		{
			for (int i = 0 ; i < texts.Length; i++)
			{
				if (texts[i] != "")
				{
					text.text = texts[i];
					Transition.FadeInCanvasGroup(canvasGroup, 0.5f);
				}
				
				audioSource.clip = audioclips[i];
				audioSource.PlayOneShot(audioSource.clip);

				yield return new WaitUntil(CheckIfAudioSourceIsNotPlaying);

				if (texts[i] != "")
				{
					text.text = texts[i];
					Transition.FadeOutCanvasGroup(canvasGroup, 0.3f);
				}

				yield return new WaitForSeconds(0.4f);
			}

			SceneManager.LoadScene(nextScreen);
		}

		private bool CheckIfAudioSourceIsNotPlaying()
		{
			return !audioSource.isPlaying;
		}

		#endregion
	}
}