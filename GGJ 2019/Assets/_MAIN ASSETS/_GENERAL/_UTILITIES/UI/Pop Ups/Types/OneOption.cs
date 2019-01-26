using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Utilities.UI.PopUps
{
	public class OneOption : PopUp 
	{
		// //////////////////////// //
		// ////// ATTRIBUTES ////// //
		// //////////////////////// //

		[Header ("ELEMENTS")]
		[SerializeField] private Text description;
		[SerializeField] private Text option1Text;
		[SerializeField] private Button option1Button;
		[SerializeField] private Button backgroundButton;

		// //////////////////////// //
		// ////// BEHAVIOURS ////// //
		// //////////////////////// //

		public void Initialize (string text, string option1Text, UnityAction option1)
		{
			this.description.text = text;
			this.option1Text.text = option1Text;

			this.option1Button.onClick.AddListener (() => PopUpAction (option1));
			this.backgroundButton.onClick.AddListener (() => DestroyPopUp ());
		}
	}
}