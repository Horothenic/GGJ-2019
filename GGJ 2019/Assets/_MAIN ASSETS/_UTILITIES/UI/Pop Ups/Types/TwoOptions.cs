using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Utilities.UI.PopUps
{
	public class TwoOptions : PopUp 
	{
		// //////////////////////// //
		// ////// ATTRIBUTES ////// //
		// //////////////////////// //

		[Header ("ELEMENTS")]
		[SerializeField] private Text description;
		[SerializeField] private Text option1Text;
		[SerializeField] private Text option2Text;
		[SerializeField] private Button option1Button;
		[SerializeField] private Button option2Button;

		// //////////////////////// //
		// ////// BEHAVIOURS ////// //
		// //////////////////////// //

		public void Initialize (string text, string option1Text, string option2Text, UnityAction option1, UnityAction option2)
		{
			this.description.text = text;
			this.option1Text.text = option1Text;
			this.option2Text.text = option2Text;

			this.option1Button.onClick.AddListener (() => PopUpAction (option1));
			this.option2Button.onClick.AddListener (() => PopUpAction (option2));
		}
	}
}