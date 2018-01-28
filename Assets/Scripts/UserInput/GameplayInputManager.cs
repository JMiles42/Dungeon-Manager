using System;
using System.Collections.Generic;
using System.Linq;
using JMiles42.AdvVar;
using JMiles42.Attributes;
using JMiles42.CSharpExtensions;
using JMiles42.Generics;
using JMiles42.Interfaces;
using JMiles42.UnityScriptsExtensions;
using UnityEngine;

public class GameplayInputManager: Singleton<GameplayInputManager>, IEventListening, IUpdate
{
	[SerializeField] private AdvInputAxisVariable PrimaryClick;
	[SerializeField] private AdvInputAxisVariable MiddleClick;
	[SerializeField] private AdvInputAxisVariable SecondaryClick;
	[SerializeField] private AdvInputAxisVariable ScrollWheel;

	public List<SavedTouchData> TouchList = new List<SavedTouchData>(2);
	public List<int> TouchIndexesToRemove = new List<int>(0);
	public FloatReference TimeForAlternateTouch = 0.2f;
	public FloatReference MovementForCancelTouch = 0.1f;

	[DisableEditing] public int touchCount;

	public Vector2Reference MousePosition;

	public static event Action<Vector2> OnPrimaryClick = (vec) =>
												  {
													  //Debug.Log("Primary" + a);
												  };

	public static event Action<Vector2> OnSecondaryClick = (vec) =>
													{
														//Debug.Log("Secondary" + a);
													};

	public static event Action<Vector2> OnScreenStartMove = (vec) =>
													 {
														 //Debug.Log("Screen Moved" + a);
													 };

	public static event Action<Vector2> OnScreenMoved = (vec) =>
												 {
													 //Debug.Log("Screen Moved" + a);
												 };

	public static event Action<Vector2> OnScreenEndMove = (vec) =>
												   {
													   //Debug.Log("Screen Moved" + a);
												   };

	public static event Action<float> OnScreenZoom = (f) =>
											  {
												  //Debug.Log("Screen Moved" + a);
											  };

	public void OnEnable()
	{
		Input.simulateMouseWithTouches = false;
		Input.backButtonLeavesApp = false;
		MovementForCancelTouch = Screen.dpi * MovementForCancelTouch;

		PrimaryClick.Value.OnKeyDown += OnPrimaryKeyDown;
		SecondaryClick.Value.OnKeyDown += OnSecondaryKeyDown;
		MiddleClick.Value.OnKeyDown += OnKeyMiddleDown;
		MiddleClick.Value.OnKeyUp += OnKeyMiddleUp;
		MiddleClick.Value.OnKey += OnKeyMiddle;

		ScrollWheel.Value.OnKey += OnScroll;
	}

	private void OnScroll(float amount) { OnScreenZoom.Trigger(amount); }

	public void Update()
	{
		MousePosition.Value = Input.mousePosition;
		PrimaryClick.Value.UpdateDataAndCallEvents();
		MiddleClick.Value.UpdateDataAndCallEvents();
		SecondaryClick.Value.UpdateDataAndCallEvents();
		ScrollWheel.Value.UpdateDataAndCallEvents(0f);

		if ((touchCount = Input.touchCount) == 0)
		{
			TouchList.Clear();
			TouchIndexesToRemove.Clear();
			return;
		}

		for (var i = 0; i < Input.touchCount; i++)
		{
			var touch = Input.GetTouch(i);
			CheckTouches(touch);
		}
		RemoveTouches();
	}

	private void RemoveTouches()
	{
		var fingerIds = new List<int>(Input.touches.Select(t => t.fingerId));
		for (int t = Input.touchCount - 1; t >= 0; t--)
		{
			if (TouchList.InRange(t))
				if (!fingerIds.Contains(TouchList[t].FingerID) || TouchIndexesToRemove.Contains(TouchList[t].FingerID))
					TouchList.RemoveAt(t);
		}
		TouchIndexesToRemove.Clear();
	}

	private void OnPrimaryKeyDown() { OnPrimaryClick.Trigger(Input.mousePosition.ToVector2()); }

	private void OnSecondaryKeyDown() { OnSecondaryClick.Trigger(Input.mousePosition.ToVector2()); }

	private Vector2 MouseStartPos;

	private Vector2 MouseDelta
	{
		get { return (MousePosition.Value - MouseStartPos); }
	}

	private void OnKeyMiddleDown()
	{
		MouseStartPos = MousePosition.Value;
		OnScreenStartMove.Trigger(MousePosition.Value);
	}

	private void OnKeyMiddleUp()
	{
		MouseStartPos = Vector2.zero;
		OnScreenEndMove.Trigger(MousePosition.Value);
	}

	private void OnKeyMiddle(float amount) { OnScreenMoved.Trigger(MouseDelta); }

	private void DoTouchPanCamera(SavedTouchData touch) {}

	public void OnDisable()
	{
		PrimaryClick.Value.OnKeyDown -= OnPrimaryKeyDown;
		SecondaryClick.Value.OnKeyDown -= OnSecondaryKeyDown;
		MiddleClick.Value.OnKeyDown -= OnKeyMiddleDown;
		MiddleClick.Value.OnKeyUp -= OnKeyMiddleUp;
		MiddleClick.Value.OnKey -= OnKeyMiddle;

		ScrollWheel.Value.OnKey -= OnScroll;
	}

	private void CheckTouches(Touch touch)
	{
		switch (touch.phase)
		{
			case TouchPhase.Began:
				TouchList.Add(new SavedTouchData(touch));
				break;
			case TouchPhase.Moved:
				break;
			case TouchPhase.Stationary:
				break;
			case TouchPhase.Ended:
				for (var i = 0; i < TouchList.Count; i++)
				{
					if (TouchList[i].FingerID == touch.fingerId)
					{
						CalculateTouch(TouchList[i], touch);
						TouchIndexesToRemove.Add(i);
					}
				}
				break;
			case TouchPhase.Canceled:
				break;
		}
	}

	private void CalculateTouch(SavedTouchData data, Touch newTouch)
	{
		var result = data.GetTouchEndData();

		var touchLength = CalculateTouchLength(result.HeldTime);

		//Debug.Log("Held Time: " + resualts.HeldTime + ":" + touchLength);
		switch (touchLength)
		{
			case TouchLength.Short:
				if (TouchHasNotMoved(data, newTouch, MovementForCancelTouch))
					OnPrimaryClick.Trigger(result.Data.StartPos);
				break;
			case TouchLength.Long:
				if (TouchHasNotMoved(data, newTouch, MovementForCancelTouch))
					OnSecondaryClick.Trigger(result.Data.StartPos);
				break;
		}
	}

	private static bool TouchHasNotMoved(SavedTouchData data, Touch newTouch, float movementForCancelTouch)
	{
		var dist = Vector3.Distance(data.StartPos, newTouch.position);
		if (dist >= movementForCancelTouch)
		{
			//Debug.Log("Cancel Distance: " + dist);
			return false;
		}
		return true;
	}

	public TouchLength CalculateTouchLength(float time)
	{
		if (time >= TimeForAlternateTouch)
			return TouchLength.Long;
		return TouchLength.Short;
	}

	[Serializable]
	public class SavedTouchData: IEqualityComparer<SavedTouchData>
	{
		public int FingerID;
		public float StartTime;
		public Vector2 StartPos;

		public SavedTouchData(Touch touch)
		{
			FingerID = touch.fingerId;
			StartPos = touch.position;
			StartTime = Time.time;
		}

		public TouchEndData GetTouchEndData()
		{
			var touch = new TouchEndData {Data = this, HeldTime = Time.time - StartTime};
			return touch;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;
			if (obj is Touch)
				return Equals((Touch) obj);
			var other = obj as SavedTouchData;
			return other != null && Equals(other);
		}

		public bool Equals(SavedTouchData other) { return other.FingerID == FingerID; }
		public bool Equals(Touch other) { return other.fingerId == FingerID; }

		public override int GetHashCode() { return -2035406951 + FingerID.GetHashCode(); }

		public struct TouchEndData
		{
			public bool Over;
			public SavedTouchData Data;
			public float HeldTime;
		}

		public bool Equals(SavedTouchData x, SavedTouchData y) { return (x != null) && x.Equals(y); }
		public int GetHashCode(SavedTouchData obj) { return obj.GetHashCode(); }
	}

	[Serializable]
	public enum TouchLength
	{
		Short,
		Long
	}

	public class ScreenMoving
	{
		public Vector2 StartPos;
	}
}