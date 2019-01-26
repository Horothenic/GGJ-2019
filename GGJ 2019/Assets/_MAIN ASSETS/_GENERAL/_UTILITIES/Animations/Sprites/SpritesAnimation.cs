using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Utilities animations namespace.
/// </summary>
namespace Utilities.Animation
{
	/// <summary>
	/// Sprites animation super script, for a better animation sequencing.
	/// </summary>
	public class SpritesAnimation : MonoBehaviour 
	{
		// //////////////////////// //
		// ////// ATTRIBUTES ////// //
		// //////////////////////// //

		// SPRITE TRIGGERS //

		[Header ("SPRITE TRIGGERS")]
		/// <summary> The sprite triggers. </summary>
		[SerializeField] private SpriteTrigger[] spriteTriggers;

		// SPRITE ANIMATED TRIGGERS //

		[Header ("SPRITE ANIMATED TRIGGERS")]
		/// <summary> The sprite animated triggers. </summary>
		[SerializeField] private SpriteAnimatedTrigger[] spriteAnimatedTriggers;

		// //////////////////////////// //
		// ////// INITIALIZATION ////// //
		// //////////////////////////// //

		/// <summary> Raises the enable event. </summary>
		void OnEnable ()
		{
			ResetAnimation ();

			// Set the coroutines for sprite triggers. //
			for (int i = 0; i < spriteTriggers.Length; i++)
			{
				this.StartCoroutine (TriggerAnimation (spriteTriggers [i]));
			}

			// Set the coroutines for sprite animated triggers. //
			for (int i = 0; i < spriteAnimatedTriggers.Length; i++)
			{
				this.StartCoroutine (TriggerAnimation (spriteAnimatedTriggers [i]));
			}
		}

		/// <summary> Resets the animation. </summary>
		private void ResetAnimation ()
		{
			this.StopAllCoroutines ();

			// Resets the sprite triggers. //
			for (int i = 0; i < spriteTriggers.Length; i++)
			{
				spriteTriggers [i].spriteObject.SetActive (false);
			}

			// Resets the sprite animated triggers. //
			for (int i = 0; i < spriteAnimatedTriggers.Length; i++)
			{
				spriteAnimatedTriggers [i].spriteAnimator.SetTrigger ("Reset");
			}
		}

		/// <summary> Triggers the animation. </summary>
		/// <param name="spriteTrigger">Sprite trigger.</param>
		private IEnumerator TriggerAnimation (SpriteTrigger spriteTrigger)
		{
			yield return new WaitForSeconds (spriteTrigger.delay);

			spriteTrigger.spriteObject.SetActive (true);
		}

		/// <summary> Triggers the animation. </summary>
		/// <param name="spriteTrigger">Sprite animated trigger.</param>
		private IEnumerator TriggerAnimation (SpriteAnimatedTrigger spriteAnimatedTrigger)
		{
			yield return new WaitForSeconds (spriteAnimatedTrigger.delay);

			spriteAnimatedTrigger.spriteAnimator.SetTrigger ("Start");
		}
	}

	// /////////////////////////// //
	// ////// CLASS HELPERS ////// //
	// /////////////////////////// //

	/// <summary>
	/// Sprite trigger class.
	/// </summary>
	[Serializable] public class SpriteTrigger
	{
		/// <summary> The delay to appear the sprite. </summary>
		public float delay;
		/// <summary> The sprite object to appear. </summary>
		public GameObject spriteObject;
	}

	/// <summary>
	/// Sprite animated trigger class.
	/// </summary>
	[Serializable] public class SpriteAnimatedTrigger
	{
		/// <summary> The delay to appear the sprite. </summary>
		public float delay;
		/// <summary> The sprite object to start animating. </summary>
		public Animator spriteAnimator;
	}
}