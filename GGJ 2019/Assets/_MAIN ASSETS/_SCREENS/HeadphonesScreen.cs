using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Utilities.Transitions;

namespace Screens
{
	public class HeadphonesScreen : MonoBehaviour 
	{
		#region ATTRIBUTES

		[Header("UI")]
		[SerializeField] private CanvasGroup canvasGroup;
		[SerializeField] private CanvasGroup canvasGroupHeadphones;
		[SerializeField] private CanvasGroup canvasGroupClick;
		[SerializeField] private Button button;

		[Header("AUDIO")]
		[SerializeField] private AudioSource audioSource;
		[SerializeField] private AudioClip audioClipHeadphones;
		[SerializeField] private AudioClip audioClipClick;

		[Header("SCREEN")]
		[SerializeField] private string nextScreen = "";

		#endregion

		#region INITIALIZATION
		
		void Start()
		{
			StartCoroutine(AppearCanvasGroupHeadphones());
		}

		#endregion

		#region BEHAVIOURS

		private IEnumerator AppearCanvasGroupHeadphones()
		{
			Transition.FadeInCanvasGroup (canvasGroupHeadphones, 1f, false);

			audioSource.clip = audioClipHeadphones;
			audioSource.PlayOneShot(audioSource.clip);

			yield return new WaitUntil(CheckIfAudioSourceIsNotPlaying);

			StartCoroutine(AppearCanvasGroupClick());
		}

		private IEnumerator AppearCanvasGroupClick()
		{
			Transition.FadeInCanvasGroup (canvasGroupClick, 0.7f, false);

			audioSource.clip = audioClipClick;
			audioSource.PlayOneShot(audioSource.clip);

			yield return new WaitUntil(CheckIfAudioSourceIsNotPlaying);

			SetButtonBehaviour();
		}

		private void SetButtonBehaviour()
		{
			button.onClick.AddListener(GoToNextScreen);
		}

		private void GoToNextScreen()
		{
			Transition.FadeOutCanvasGroup (canvasGroup, 0.7f, false, () => SceneManager.LoadScene(nextScreen));
		}

		private bool CheckIfAudioSourceIsNotPlaying()
		{
			return !audioSource.isPlaying;
		}

		#endregion
	}
}