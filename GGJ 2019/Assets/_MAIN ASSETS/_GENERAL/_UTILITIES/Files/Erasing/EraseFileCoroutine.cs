using UnityEngine;
using System.Collections;

namespace Utilities.Files.Erasing
{
	public class EraseFileCoroutine : MonoBehaviour
	{
		FileEraseJob job;

		void Start()
		{
			DontDestroyOnLoad (this.gameObject);
		}

		public void SetDataAndStart(string completeRoute)
		{
			job = new FileEraseJob();
			job.SetData (completeRoute);
			job.Start ();
			StartCoroutine (Wait ());
		}

		public IEnumerator Wait ()
		{
			yield return StartCoroutine (job.WaitFor());
			DestroyImmediate (this.gameObject);
		}
	}
}