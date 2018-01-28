using JMiles42;
using JMiles42.AdvVar;
using JMiles42.AdvVar.RuntimeRef;
using JMiles42.CSharpExtensions;
using JMiles42.Curves.Components;
using JMiles42.UnityScriptsExtensions;
using JMiles42.Utilities;
using UnityEngine;

public class CameraMotor: JMilesBehavior
{
	public CameraRTRef Camera;
	public TransformRTRef CameraHolder;
	public Transform LookAt;
	public Vector2Reference MousePosition;
	public FloatReference Speed = 1;

	public TransformData StartData;
	public FloatReference YAxisLookAt = 1;

	[SerializeField] private BezierCurveV3DBehaviour zoomCurve;

	[SerializeField] private FloatReference zoomLevel = 1f;

	public FloatReference zoomMovement = 0.1f;

	public FloatReference zoomRate = 1;

	private void OnEnable()
	{
		GameplayInputManager.OnScreenZoom += ScreenZoom;
		GameplayInputManager.OnScreenStartMove += ScreenStartMove;
		GameplayInputManager.OnScreenEndMove += ScreenEndMove;
		GameplayInputManager.OnScreenMoved += ScreenMoved;
	}

	private void Start()
	{
		ScreenZoom(zoomLevel);
	}

	private void OnDisable()
	{
		GameplayInputManager.OnScreenZoom -= ScreenZoom;
		GameplayInputManager.OnScreenStartMove -= ScreenStartMove;
		GameplayInputManager.OnScreenEndMove -= ScreenEndMove;
		GameplayInputManager.OnScreenMoved -= ScreenMoved;
	}

	private void ScreenStartMove(Vector2 mouseDelta)
	{
		StartData = CameraHolder.Reference.transform;
	}

	private void ScreenMoved(Vector2 mouseDelta)
	{
		var pos = StartData.Position + (Camera.Reference.transform.TransformDirection(mouseDelta).FromX_Y2Z() * Speed).SetY(0);
		//CameraHolder.Reference.position = new Vector3(pos.x.Clamp(minPos.x, maxPos.x), pos.y, pos.z.Clamp(minPos.z, maxPos.z));
		CameraHolder.Reference.position = pos;
	}

	private void ScreenEndMove(Vector2 mouseDelta)
	{ }

	private void ScreenZoom(float f)
	{
		f = f * zoomRate;
		zoomLevel = (zoomLevel + f).Clamp();
		Camera.Reference.transform.position = zoomCurve.Lerp(zoomLevel);

		Camera.Reference.transform.LookAt(LookAt);
	}
}