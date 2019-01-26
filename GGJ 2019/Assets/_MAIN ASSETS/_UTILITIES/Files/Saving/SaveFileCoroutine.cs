using UnityEngine;
using System.Collections;

namespace Utilities.Files.Saving
{
	public class SaveFileCoroutine : MonoBehaviour
	{
		FileSaveJob job;

		void Start()
		{
			DontDestroyOnLoad (this.gameObject);
		}

		public void SetDataAndStart(string completeRoute, byte[] data)
		{
			job = new FileSaveJob();
			job.SetData(completeRoute, data);
			job.Start ();
			StartCoroutine (Wait ());
		}

		public IEnumerator Wait ()
		{
			yield return StartCoroutine (job.WaitFor());
			Destroy (this.gameObject);
		}
	}
}