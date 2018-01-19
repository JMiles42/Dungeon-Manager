using JMiles42.Editor;
using JMiles42.Editor.Utilities;
using JMiles42.Generics;
using JMiles42.UnityScriptsExtensions;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoomSO))]
public class RoomEditor: JMilesEditorBase<RoomSO>
{
	private const float BUTTON_SIZE = 32;
	private const float BORDER_SIZE = 64f;

	//public Tile[] Tiles
	//{
	//	get { return Target.Data.Tiles; }
	//	set { Target.Data.Tiles = value; }
	//}

	public override void OnInspectorGUI()
	{
		var prop = GetTilesArray();
		EditorGUILayout.LabelField("Room Layout");
		CheckProp(prop);
		Buttons();
		//DoLayoutGUI(prop);
		DoGUI(prop);
	}

	private void Buttons()
	{
		using(EditorDisposables.HorizontalScope())
		{
			var resetBtn = JMilesGUILayoutEvents.Button("Empty Room");
			var fillBtn = JMilesGUILayoutEvents.Button("Fill Room");

			if(resetBtn)
				ResetRoom();
			else if(fillBtn)
				ResetRoom(1);
		}
	}

	private static void CheckProp(SerializedProperty prop)
	{
		if(prop.arraySize != Room.TOTAL_SIZE)
		{
			prop.arraySize = Room.TOTAL_SIZE;
			Debug.Log("Checked and Changed Room Array Size");
		}
	}

	private static void DoLayoutGUI(SerializedProperty prop)
	{
		using(EditorDisposables.VerticalScope())
		{
			for(var x = 0; x < Room.SIZE; x++)
			{
				using(EditorDisposables.HorizontalScope())
				{
					for(var y = 0; y < Room.SIZE; y++)
					{
						var i = Array2DHelpers.Get1DIndexOf2DCoords(Room.SIZE, x, y);
						var iProp = prop.GetArrayElementAtIndex(i);
						iProp.Next(true);

						using(EditorDisposables.ColorChanger(iProp.enumValueIndex == 0?
																 Color.blue :
																 Color.green))
							DrawButtons(iProp);
					}
				}
			}
		}
	}

	private static void DoGUI(SerializedProperty prop)
	{
		var rect = EditorGUILayout.GetControlRect(false, 16f, GUILayout.ExpandWidth(true));
		rect.height = rect.width;

		rect = rect.MoveY(BORDER_SIZE).ChangeX(BORDER_SIZE).MoveWidth(-BORDER_SIZE);
		var btnSize = rect.width / Room.SIZE;

		//var totalHeight = (btnSize * Room.SIZE) + (BORDER_SIZE * 2);

		var horizontalRect = new Rect(rect);

		for(var x = 0; x < Room.SIZE; x++)
		{
			horizontalRect.width = btnSize;
			horizontalRect.height = btnSize;

			for(var y = 0; y < Room.SIZE; y++)
			{
				var finalRect = horizontalRect;

				var iProp = GetEnumPropFromArrayProp(prop, x, y);

				using(EditorDisposables.ColorChanger(iProp.enumValueIndex == 0?
														 Color.blue :
														 Color.green))
					DrawButtons(finalRect, iProp);
				horizontalRect = horizontalRect.MoveX(btnSize);
			}
			horizontalRect = horizontalRect.MoveY(btnSize).SetX(rect.x);
		}
	}

	private static void DrawButtons(SerializedProperty iProp)
	{
		var guiContent = iProp.enumValueIndex == 0?
			new GUIContent("E", "Empty Tile") :
			new GUIContent("F", "Floor Tile");
		var EmptyTileBtn = JMilesGUILayoutEvents.Button(guiContent, GUILayout.Width(BUTTON_SIZE), GUILayout.Height(BUTTON_SIZE));
		if(EmptyTileBtn)
		{
			iProp.enumValueIndex = iProp.enumValueIndex ^ 1;
			iProp.serializedObject.ApplyModifiedProperties();
		}
	}

	private static void DrawButtons(Rect pos, SerializedProperty iProp)
	{
		var guiContent = iProp.enumValueIndex == 0?
			new GUIContent("E", "Empty Tile") :
			new GUIContent("F", "Floor Tile");
		var EmptyTileBtn = JMilesGUIEvents.Button(pos, guiContent);
		if(EmptyTileBtn)
		{
			iProp.enumValueIndex = iProp.enumValueIndex ^ 1;
			iProp.serializedObject.ApplyModifiedProperties();
		}
	}

	private void ResetRoom(int index = 0)
	{
		var prop = GetTilesArray();
		for(var x = 0; x < Room.SIZE; x++)
		{
			for(var y = 0; y < Room.SIZE; y++)
			{
				var iProp = GetEnumPropFromArrayProp(prop, x, y);
				iProp.enumValueIndex = index;
			}
		}
		prop.serializedObject.ApplyModifiedProperties();
	}

	private static SerializedProperty GetEnumPropFromArrayProp(SerializedProperty prop, int x, int y)
	{
		var i = Array2DHelpers.Get1DIndexOf2DCoords(Room.SIZE, x, y);
		var iProp = prop.GetArrayElementAtIndex(i);
		iProp.Next(true);
		return iProp;
	}

	private SerializedProperty GetTilesArray()
	{
		var prop = serializedObject.FindProperty("Data");
		prop.Next(true);
		return prop;
	}
}