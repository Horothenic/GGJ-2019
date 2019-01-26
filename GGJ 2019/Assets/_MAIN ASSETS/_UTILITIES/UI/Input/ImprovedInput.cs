using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

using Utilities.Transitions;

namespace Utilities.UI
{
	public class ImprovedInput : MonoBehaviour, IPointerClickHandler
	{
		#region ATTRIBUTES

		[Header ("COMPONENTS")]
		[SerializeField] private Text text;
		[SerializeField] private Text placeholder;
		[SerializeField] private GameObject caret;

		[Header ("PANELS")]
		[SerializeField] private RectTransform movePanel;
		[SerializeField] private float amount;
		[SerializeField] private float time;

		[Header ("TWEAKS")]
		[SerializeField] private bool isPassword;
		[SerializeField] private string character = "*";
		[SerializeField] private bool isMultiline;
		[SerializeField] private int characterLimit = 30;

		[Header ("STATUS")]
		[SerializeField] private bool keyboardActive = false;
		[SerializeField] public string storedText;

		private Action onEndedInput = null;
		
		private TouchScreenKeyboard keyboard;
		private TouchScreenKeyboardType keyboardType;

		#endregion

		#region INITIALIZATION

		void Start ()
		{
			TouchScreenKeyboard.hideInput = true;
			keyboardType = isPassword ? TouchScreenKeyboardType.NamePhonePad : TouchScreenKeyboardType.EmailAddress;

			caret.SetActive (false);
		}

		public void SetOnEndedInput (Action onEndedInput)
		{
			this.onEndedInput = onEndedInput;
		}

		public void ResetInput ()
		{
			storedText = "";
			text.text = "";

			placeholder.gameObject.SetActive (true);
		}

		#endregion

		#region UPDATES

		void FixedUpdate ()
		{
			if (keyboardActive && keyboard.text.Length != storedText.Length)
			{
				if (keyboard.text == "")
				{
					placeholder.gameObject.SetActive (true);
					text.gameObject.SetActive (false);

					storedText = "";
				}
				else
				{
					placeholder.gameObject.SetActive (false);
					text.gameObject.SetActive (true);
					
					storedText = FilterNewString (keyboard.text);

					text.text = isPassword ? CreatePasswordString (storedText.Length) : storedText;
				}
			}
		}

		#endregion

		#region EVENTS
		
		public void OnPointerClick (PointerEventData eventData)
		{
			#if !UNITY_EDITOR
			AppearTouchKeyboard ();
			#endif
		}

		#endregion

		#region BEHAVIOURS

		void AppearTouchKeyboard ()
		{
			//ApplicationManager.Instance.CurrentEventSystem.enabled = false;

			string showText = isPassword ? CreatePasswordString (storedText.Length) : storedText;
			keyboard = TouchScreenKeyboard.Open (showText, keyboardType, false, isMultiline, isPassword, false, "", characterLimit);

			Transition.MoveRectTransformVertically (movePanel, amount, time);
			caret.SetActive (true);

			keyboardActive = true;

			StartCoroutine (KeyboardActive ());
		}

		IEnumerator KeyboardActive ()
		{
			yield return new WaitUntil (EndedInput);

			Transition.MoveRectTransformVertically (movePanel, -movePanel.anchoredPosition.y, time);
			caret.SetActive (false);

			keyboardActive = false;
			//ApplicationManager.Instance.CurrentEventSystem.enabled = true;

			if (onEndedInput != null)
				onEndedInput ();
		}

		private bool EndedInput ()
		{
			return 	keyboard.status != TouchScreenKeyboard.Status.Visible;
		}

		private string FilterNewString (string keyboardText)
		{
			string temp = storedText;

			if (Mathf.Abs (storedText.Length - temp.Length) == 1)
			{
				if (keyboardText.Length > temp.Length)
					temp += keyboardText.Substring (keyboardText.Length - 1);
				else
					temp = temp.Substring (0, storedText.Length - 1);
			}
			else
			{
				temp = keyboardText;
			}

			return temp;
		}

		private string CreatePasswordString (int length)
		{
			string passwordText = "";
			for (int i = 0; i < storedText.Length; i++)
				passwordText += character;
			return passwordText;
		}

		#endregion
	}
}