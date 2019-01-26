using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.UI.PopUps
{
	public class PopUpsManager : MonoBehaviour 
	{
		// //////////////////////// //
		// ////// ATTRIBUTES ////// //
		// //////////////////////// //

		private static PopUpsManager instance;
		public static PopUpsManager Instance
		{
			get
			{
				return instance;
			}
		}

		[Header ("POPUPS")]
		[SerializeField] private OneOption oneOption;
		[SerializeField] private TwoOptions twoOptions;

		// //////////////////////////// //
		// ////// INITIALIZATION ////// //
		// //////////////////////////// //

		void Awake ()
		{
			instance = this;
		}

		// //////////////////////// //
		// ////// BEHAVIOURS ////// //
		// //////////////////////// //

		public static void CreateOneOptionPopUp (string description, string option1Text, UnityAction option1)
		{
			OneOption popUp = (OneOption) Instantiate (Instance.oneOption);

			popUp.Initialize (description, option1Text, option1);
		}

		public static void CreateTwoOptionsPopUp (string description, string option1Text, string option2Text, UnityAction option1, UnityAction option2)
		{
			TwoOptions popUp = (TwoOptions) Instantiate (Instance.twoOptions);

			popUp.Initialize (description, option1Text, option2Text, option1, option2);
		}
	}

	public enum Types
	{
		OneOption,
		TwoOptions
	}
}