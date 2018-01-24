using System.Collections.Generic;
using JMiles42;
using JMiles42.Attributes;
using UnityEngine;

public class InputModeManager: JMilesBehavior
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
		set { SwitchMode(value); }
	}

	public void SwitchMode(int index)
	{
		for(var i = 0; i < modes.Count; i++)
			modes[i].gameObject.SetActive(index == i);
		_activeMode = index;
	}

	private void Start()
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
}