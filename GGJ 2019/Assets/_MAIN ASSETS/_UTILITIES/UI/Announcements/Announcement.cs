using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Utilities.Transitions;

namespace Utilities.Announcements
{
	[RequireComponent (typeof (CanvasGroup))]
	public class Announcement : MonoBehaviour 
	{
		public Text text;
		public CanvasGroup canvasGroup;

		public void Initialize (string text, float duration)
		{
			this.text.text = text;

			Transition.FadeInCanvasGroup (canvasGroup, 0.2f, true, () => StartCoroutine (EndAnnouncement (duration)));
		}

		private IEnumerator EndAnnouncement (float duration)
		{
			yield return new WaitForSeconds (duration);

			Transition.FadeOutCanvasGroup (canvasGroup, 0.2f, true, () => Destroy (gameObject));
		}
	}
}