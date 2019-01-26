using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Semantic
{
	public class SemanticField : MonoBehaviour 
	{
		#region ATTRIBUTES

		[Header("SEMANTIC")]
		[SerializeField] private SemanticFields semanticFields;

		#endregion

		#region PROPERTIES

		public SemanticFields GetSemanticField { get { return semanticFields; } }

		#endregion

		#region BEHAVIOURS



		#endregion
	}
}