using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

		#region PROPERTIES

		public string CurrentQuest { get { return currentQuest; } }

		public int[] Top3SemanticFieldsIndexes
		{
			get
			{
				var indexArray = semanticData
                   .Select((value, index) => new { value, index })
                   .OrderByDescending(item => item.value)
                   .Take(3)
                   .Select(item => item.index)
                   .ToArray();
				return indexArray;
			}
		}

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

		public void ChangeQuest()
		{
			semanticData = new int[10];
			currentQuest = quests[1];
		}

		public void IncrementData (SemanticFields type, int increment)
		{
			semanticData[(int)type] += increment;
		}

		#endregion
	}
}