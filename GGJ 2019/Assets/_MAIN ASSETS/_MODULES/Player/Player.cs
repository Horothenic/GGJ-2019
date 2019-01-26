using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utilities.Inspector;

using Semantic;

namespace Game
{
	public class Player : MonoBehaviour 
	{
		#region ATTRIBUTES

		[Header("SEMANTIC")]
		[ReadOnly] [SerializeField] private SemanticFields currentSemanticField = SemanticFields.Street;

		#endregion

		#region INITIALIZATION



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
			if (collider.tag == "Player")
			{
				SemanticField semanticField = collider.gameObject.GetComponentInChildren<SemanticField>();

				if (currentSemanticField == semanticField.GetSemanticField)
					currentSemanticField = SemanticFields.Street;
			}
		}

		#endregion
	}
}