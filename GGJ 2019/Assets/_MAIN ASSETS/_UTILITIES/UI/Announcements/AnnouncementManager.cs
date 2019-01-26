using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Announcements
{
	public class AnnouncementManager : MonoBehaviour 
	{	
		#region INSTANCE

		private static AnnouncementManager instance;

		#endregion

		#region ATTRIBUTES

		[Header ("ANNOUNCEMENTS")]
		[SerializeField] private Announcement announcementPrefab;

		#endregion

		#region INITIALIZATION

		void Awake ()
		{
			instance = this;
		}

		#endregion

		#region BEHAVIOURS

		public static void CreateAnnouncement (string text, float duration = 2f)
		{
			Instantiate (instance.announcementPrefab).Initialize (text, duration);
		}

		#endregion
	}
}