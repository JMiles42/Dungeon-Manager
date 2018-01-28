using JMiles42;
using JMiles42.JUI.Button;
using UnityEngine;

public class ModeSwitcherButton: JMilesBehavior
{
	[SerializeField] private ButtonClickEventBase _buttonClickEventBase;

	public int index;

	public ButtonClickEventBase ButtonClickEventBase
	{
		get { return _buttonClickEventBase ?? (_buttonClickEventBase = GetComponent<ButtonClickEventBase>()); }
		set { _buttonClickEventBase = value; }
	}

	private void OnEnable()
	{
		ButtonClickEventBase.onMouseClick += OnMouseClick;
		InputModeManager.OnModeSwitch += OnModeSwitch;
	}

	private void OnDisable()
	{
		ButtonClickEventBase.onMouseClick -= OnMouseClick;
		InputModeManager.OnModeSwitch -= OnModeSwitch;
	}

	private void OnModeSwitch(int i)
	{
		ButtonClickEventBase.Button.interactable = index != i;
	}

	private void OnMouseClick()
	{
		InputModeManager.SwitchMode(index);
	}
}