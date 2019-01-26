using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Transforms
{
	public class DestroyBelowTransform : MonoBehaviour 
	{
		public Transform trigger;

		private void Update ()
		{
			if (transform.position.y < trigger.position.y)
			{
				Destroy (gameObject);
			}
		}
	}
}