using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utilities.Inspector;

using Semantic;

namespace Quests
{
	public class QuestManager : MonoBehaviour 
	{
		#region ATTRIBUTES

		[Header("QUESTS")]
		[SerializeField] private string[] quests;
		[ReadOnly] [SerializeField] private string currentQuest;

		[Header("DATA")]
		[ReadOnly] [SerializeField] private int[] semanticData;

		public delegate void SemanticDelegate (SemanticFields type, int increment);
		public static SemanticDelegate AddSemanticData;

		#endregion

		#region INITIALIZATION

		void Awake()
		{
			AddSemanticData += IncrementData;
		}

		void OnDestroy()
		{
			AddSemanticData -= IncrementData;
		}

		void Start()
		{
			semanticData = new int[10];
			currentQuest = quests[0];
		}

		#endregion

		#region BEHAVIOURS

		private void ChangeQuest()
		{
			semanticData = new int[10];
			currentQuest = quests[Random.Range(1, quests.Length)];
		}

		public void IncrementData (SemanticFields type, int increment)
		{
			semanticData[(int)type] += increment;
		}

		#endregion
	}
}