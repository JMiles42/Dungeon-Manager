using JMiles42.Editor;
using JMiles42.Editor.Utilities;
using JMiles42.Generics;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoomSO))]
public class RoomEditor: JMilesEditorBase<RoomSO>
{
	public Tile[] Tiles
	{
		get { return Target.Data.Tiles; }
		set { Target.Data.Tiles = value; }
	}

	//public override void DrawGUI()
	public override void OnInspectorGUI()
	{
		var prop = serializedObject.FindProperty("Data");
		prop.Next(true);
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

						if(iProp.enumValueIndex == 0)
						{
							using(EditorDisposables.HorizontalScope(GUI.skin.box))
							{
								using(EditorDisposables.ColorChanger(Color.blue))
									DrawButtons(iProp);
							}
						}
						else if(iProp.enumValueIndex == 1)
						{
							using(EditorDisposables.HorizontalScope(GUI.skin.box))
							{
								using(EditorDisposables.ColorChanger(Color.green))
									DrawButtons(iProp);
							}
						}
						else
						{
							using(EditorDisposables.HorizontalScope(GUI.skin.box))
								DrawButtons(iProp);
						}
					}
				}
			}
		}
	}

	private static void DrawButtons(SerializedProperty iProp)
	{
		var EmptyTileBtn = JMilesGUILayoutEvents.Button(new GUIContent("E", "Empty Tile"));
		var FloorTileBtn = JMilesGUILayoutEvents.Button(new GUIContent("F", "Floor Tile"));
		if(EmptyTileBtn)
		{
			iProp.enumValueIndex = 0;
			iProp.serializedObject.ApplyModifiedProperties();
		}
		if(FloorTileBtn)
		{
			iProp.enumValueIndex = 1;
			iProp.serializedObject.ApplyModifiedProperties();
		}
	}

	private void ResetRoom()
	{ }
}