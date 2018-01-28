using System;
using System.Collections.Generic;
using JMiles42.Attributes;
using JMiles42.CSharpExtensions;
using JMiles42.Generics;
using UnityEngine;

public class InputModeManager: Singleton<InputModeManager>
{
	[SerializeField] [GetSetter("ActiveMode")] private int _activeMode;

	[SerializeField] private List<InputMode> modes = new List<InputMode>();

	public List<InputMode> Modes
	{
		get { return modes; }
		private set { modes = value; }
	}

	public int ActiveMode
	{
		get { return _activeMode; }
		set { Switch(value); }
	}
	public static Action<int> OnModeSwitch;

	private void OnEnable()
	{
		if(Modes.Count == 0)
			ActiveMode = -1;
		else
			SwitchMode(ActiveMode);
	}

	private void Reset()
	{
		for(var i = 0; i < transform.childCount; i++)
			transform.GetChild(i).gameObject.SetActive(true);
		modes = new List<InputMode>(GetComponentsInChildren<InputMode>());
	}

	public void Switch(int index)
	{
		for(var i = 0; i < modes.Count; i++)
			modes[i].gameObject.SetActive(index == i);
		OnModeSwitch.Trigger(index);
		_activeMode = index;
	}

	public static void SwitchMode(int index)
	{
		Instance.Switch(index);
	}
}