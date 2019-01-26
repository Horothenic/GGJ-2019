using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
	public class AnalyticsManager : MonoBehaviour 
	{
		#region INSTANCE

		private static AnalyticsManager instance;
		private static AnalyticsManager Instance { get { return instance; }}

		#endregion

		#region ATTRIBUTES
		
		

		#endregion

		#region INITIALIZATION

		void Awake ()
		{
			instance = this;
		}

		#endregion

		#region BEHAVIOURS

		public static void SendCustomEvent (string customEvent, Dictionary <string, object> parameters = null)
		{
			if (parameters == null)
			{
				UnityEngine.Analytics.Analytics.CustomEvent (customEvent.ToString ());
			}
			else
			{
				UnityEngine.Analytics.Analytics.CustomEvent (customEvent.ToString (), parameters);
			}
		}

		#endregion
	}
}