using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utilities.Transitions;

/// <summary>
/// GameObject helpers namespace.
/// </summary>
namespace Helpers.GameObject
{
	/// <summary>
	/// Moves up and down a gameObject.
	/// </summary>
	public class MoveUpAndDown : MonoBehaviour 
	{
		// //////////////////////// //
		// ////// ATTRIBUTES ////// //
		// //////////////////////// //

		/// <summary> The interval on time that the gameObject use to move up or down (The tinier the faster). </summary>
		[SerializeField] private float interval = 1;
		/// <summary> The distance that the gameObject will move. </summary>
		[SerializeField] private float distance = 10;

		// ////////////////////////////// //
		// ////// MOVING BEHAVIOUR ////// //
		// ////////////////////////////// //

		/// <summary> Starts this instance. </summary>
		void Start ()
		{
			MoveUp ();
		}

		/// <summary> Moves up the gameObject. </summary>
		void MoveUp ()
		{
			Transition.MoveTransformVertically (this.gameObject.transform, distance, interval, false, iTween.EaseType.easeInOutQuad, 0, MoveDown);
		}

		/// <summary> Moves down the gameObject. </summary>
		void MoveDown ()
		{
			Transition.MoveTransformVertically (this.gameObject.transform, -distance, interval, false, iTween.EaseType.easeInOutQuad, 0, MoveUp);
		}
	}
}