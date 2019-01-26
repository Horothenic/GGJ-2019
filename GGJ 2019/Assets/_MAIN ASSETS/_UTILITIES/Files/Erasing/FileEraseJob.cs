using UnityEngine;
using System.Collections;
using System.IO;

namespace Utilities.Files.Erasing
{
	public class FileEraseJob : ThreadedJob
	{
		string completeRoute;
		byte[] data;

		public void SetData(string completeRoute)
		{
			this.completeRoute = completeRoute;
		}

		protected override void ThreadFunction()
		{
			if (Directory.Exists (completeRoute)) 
			{
				Debug.Log ("ERASING FOLDER: " + completeRoute);
				Directory.Delete (completeRoute, true);
			}
			else if (File.Exists (completeRoute))
			{
				Debug.Log ("ERASING FILE: " + completeRoute);
				File.Delete (completeRoute);
			}
			else
			{
				Debug.Log ("ERROR ERASING: " + completeRoute);
			}
		}
	}
}