using UnityEngine;
using System.Collections;
using System.IO;

namespace Utilities.Files.Saving
{
	public class FileSaveJob : ThreadedJob
	{
		string completeRoute;
		byte[] data;

		public void SetData(string completeRoute, byte[] data)
		{
			this.completeRoute = completeRoute;
			this.data = data;
		}

		protected override void ThreadFunction()
		{
			File.WriteAllBytes (completeRoute, data);
		}
	}
}