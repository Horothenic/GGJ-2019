using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Utilities.Transitions;

using Game;
using CustomInputs;
using Quests;

namespace Results
{
	public class ResultsScreen : MonoBehaviour 
	{
		#region ATTRIBUTES

		[Header("REFERENCES")]
		[SerializeField] private Player player;
		[SerializeField] private QuestManager questManager;
		[SerializeField] private AudioSource audioSource;

		[Header("AUDIOS")]
		[SerializeField] private AudioClip[] questMessage;
		[SerializeField] private AudioClip[] semanticFirst;
		[SerializeField] private AudioClip[] semanticSecond;
		[SerializeField] private AudioClip[] semanticThird;
		[SerializeField] private AudioClip reflexion;
		[SerializeField] private AudioClip quest2Message;
		[SerializeField] private AudioClip tap;

		[Header("TEXTS")]
		[SerializeField] private Text questMessageText;
		[SerializeField] private Text semanticFirstText;
		[SerializeField] private Text semanticSecondText;
		[SerializeField] private Text semanticThirdText;
		[SerializeField] private Text reflexionText;
		[SerializeField] private Text quest2MessageText;

		[Header("CANVAS GROUPS")]
		[SerializeField] private CanvasGroup questMessageCanvasGroup;
		[SerializeField] private CanvasGroup semanticFirstCanvasGroup;
		[SerializeField] private CanvasGroup semanticSecondCanvasGroup;
		[SerializeField] private CanvasGroup semanticThirdCanvasGroup;
		[SerializeField] private CanvasGroup reflexionCanvasGroup;
		[SerializeField] private CanvasGroup quest2MessageCanvasGroup;

		[Header("UI")]
		[SerializeField] private CanvasGroup blackScreen;

		private bool buttonEnable = false;

		#endregion

		#region INITIALIZATION

		void Awake()
		{
			InputManager.LongPressEvent += ShowResults;
		}

		void OnDestroy()
		{
			InputManager.LongPressEvent -= ShowResults;
		}

		#endregion

		#region UPDATES

		void Update()
		{
			if (buttonEnable && Input.GetKeyDown(KeyCode.Space))
			{
				ReloadGame();
			}
		}

		#endregion

		#region BEHAVIOURS

		private void ShowResults()
		{
			player.DisableMovement();

			Transition.FadeInCanvasGroup (blackScreen, 0.4f, true, () => StartCoroutine(MessageSequence()));
		}

		private IEnumerator MessageSequence()
		{
			player.MoveAway();

			// Quest title //

			questMessageText.text = "El " + questManager.CurrentQuest + " para ti suena a:";
			Transition.FadeInCanvasGroup(questMessageCanvasGroup, 0.3f);

			audioSource.clip = questMessage[questManager.CurrentQuest == "hogar" ? 0 : 1];
			audioSource.PlayOneShot(audioSource.clip);
			yield return new WaitUntil(CheckIfAudioSourceIsNotPlaying);

			// Top 3 //

			int[] messagesIndexes = questManager.Top3SemanticFieldsIndexes;

			semanticFirstText.text = ((Semantic.SemanticFields)messagesIndexes[0]).ToString();
			Transition.FadeInCanvasGroup(semanticFirstCanvasGroup, 0.3f);

			audioSource.clip = semanticFirst[messagesIndexes[0]];
			audioSource.PlayOneShot(audioSource.clip);
			yield return new WaitUntil(CheckIfAudioSourceIsNotPlaying);

			semanticSecondText.text = "Con " + ((Semantic.SemanticFields)messagesIndexes[1]).ToString().ToLower();
			Transition.FadeInCanvasGroup(semanticSecondCanvasGroup, 0.3f);

			audioSource.clip = semanticSecond[messagesIndexes[1]];
			audioSource.PlayOneShot(audioSource.clip);
			yield return new WaitUntil(CheckIfAudioSourceIsNotPlaying);

			semanticThirdText.text = "y un poquito de " + ((Semantic.SemanticFields)messagesIndexes[2]).ToString().ToLower();
			Transition.FadeInCanvasGroup(semanticThirdCanvasGroup, 0.3f);

			audioSource.clip = semanticThird[messagesIndexes[2]];
			audioSource.PlayOneShot(audioSource.clip);
			yield return new WaitUntil(CheckIfAudioSourceIsNotPlaying);

			// Relexion //

			Transition.FadeInCanvasGroup(reflexionCanvasGroup, 0.3f);

			audioSource.clip = reflexion;
			audioSource.PlayOneShot(audioSource.clip);
			yield return new WaitUntil(CheckIfAudioSourceIsNotPlaying);

			// Next Quest //

			Transition.FadeInCanvasGroup(quest2MessageCanvasGroup, 0.3f);

			audioSource.clip = quest2Message;
			audioSource.PlayOneShot(audioSource.clip);
			yield return new WaitUntil(CheckIfAudioSourceIsNotPlaying);

			buttonEnable = true;
		}

		private void HideAllCanvasGroups()
		{
			Transition.FadeOutCanvasGroup(questMessageCanvasGroup, 0.25f);
			Transition.FadeOutCanvasGroup(semanticFirstCanvasGroup, 0.25f);
			Transition.FadeOutCanvasGroup(semanticSecondCanvasGroup, 0.25f);
			Transition.FadeOutCanvasGroup(semanticThirdCanvasGroup, 0.25f);
			Transition.FadeOutCanvasGroup(reflexionCanvasGroup, 0.25f);
			Transition.FadeOutCanvasGroup(quest2MessageCanvasGroup, 0.25f);

			Transition.FadeOutCanvasGroup(blackScreen, 0.4f);
		}

		private void ReloadGame ()
		{
			audioSource.clip = tap;
			audioSource.PlayOneShot(audioSource.clip);

			buttonEnable = false;

			questManager.ChangeQuest();

			player.ResetPosition();
			player.EnableMovement();

			HideAllCanvasGroups();
		}
		
		private bool CheckIfAudioSourceIsNotPlaying()
		{
			return !audioSource.isPlaying;
		}

		#endregion
	}
}