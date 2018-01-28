using JMiles42.Editor;
using UnityEditor;

[CustomEditor(typeof(MapSO))]
public class MapSOEditor: JMilesEditorBase<MapSO>
{
	public override void DrawGUI()
	{
		if(JMilesGUILayoutEvents.Button("Generate Map"))
		{
			Target.Data = Generator.GenerateMap(Target.Settings);
			EditorUtility.SetDirty(Target);
		}
	}
}