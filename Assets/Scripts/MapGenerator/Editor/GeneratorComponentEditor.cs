using JMiles42.Editor;
using UnityEditor;

[CustomEditor(typeof(GeneratorComponent))]
public class GeneratorComponentEditor: JMilesEditorBase<GeneratorComponent>
{
	public override void DrawGUI()
	{
		if(!Target.Map)
			return;
		if(JMilesGUILayoutEvents.Button("Generate Map"))
		{
			Target.Map.Data = Generator.GenerateMap(Target.Settings);
			EditorUtility.SetDirty(Target.Map);
		}
	}
}