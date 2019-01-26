using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.SceneManagement
{
	public class ChangeSceneOnAnimatorEvent : MonoBehaviour 
	{
		[SerializeField] private int sceneID = 0;
		[SerializeField] private float time = 1f;

		public void Start()
		{
			AnimationClip clip;
			Animator anim;

			// new event created
			AnimationEvent evt;
			evt = new AnimationEvent();

			evt.intParameter = sceneID;
			evt.time = time;
			evt.functionName = "TriggerNewScene";

			anim = GetComponent<Animator>();

			clip = anim.runtimeAnimatorController.animationClips [0];
			clip.AddEvent(evt);
		}

		public void TriggerNewScene (int sceneID)
		{
			if (sceneID >= 0)
				UnityEngine.SceneManagement.SceneManager.LoadScene (sceneID);
			else
				Debug.LogWarning ("Invalid scene ID");
		}
	}
}