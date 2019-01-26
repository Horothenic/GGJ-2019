using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utilities.Inspector;

using Semantic;
using CustomInputs;

namespace Game
{
	public class Player : MonoBehaviour 
	{
		#region ATTRIBUTES

		[Header("SEMANTIC")]
		[ReadOnly] [SerializeField] private SemanticFields currentSemanticField = SemanticFields.Street;

		#endregion

		#region INITIALIZATION

		void Awake()
		{
			InputManager.DoubleTapEvent += SaveSemanticFieldEvent;
		}

		void OnDestroy()
		{
			InputManager.DoubleTapEvent -= SaveSemanticFieldEvent;
		}

		#endregion

		#region BEHAVIOURS

		private void SaveSemanticFieldEvent ()
		{
			Debug.Log (currentSemanticField + " liked!");
		}

		#endregion

		#region COLLISION_BEHAVIOURS

		void OnTriggerEnter(Collider collider)
		{
			if (collider.tag == "Semantic Field")
			{
				SemanticField semanticField = collider.gameObject.GetComponentInChildren<SemanticField>();

				currentSemanticField = semanticField.GetSemanticField;
			}
		}

		void OnTriggerExit(Collider collider)
		{
			if (collider.tag == "Semantic Field")
			{
				SemanticField semanticField = collider.gameObject.GetComponentInChildren<SemanticField>();

				if (currentSemanticField == semanticField.GetSemanticField)
					currentSemanticField = SemanticFields.Street;
			}
		}

		#endregion
	}
}