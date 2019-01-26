using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BasicDelegates 
{
	public delegate void VoidDelegate ();
	public delegate void StringDelegate (string myString);
	public delegate void IntDelegate (int myInt);
	public delegate void BoolDelegate (bool myBool);
	public delegate void FloatDelegate (float myFloat);
	public delegate void ObjectDelegate (object myObject);

	public delegate void IntIntDelegate (int one, int two);
	public delegate void StringIntDelegate (string one, int two);

	public delegate void StringArrayDelegate (string[] myStringArray);

	public delegate string VoidDelegate_String ();
	public delegate Sprite VoidDelegate_Sprite ();

	public delegate Transform VoidDelegate_Transform ();
}
