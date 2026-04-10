using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Linq;

[CustomPropertyDrawer(typeof(ISkill), true)]
public class SkillEffectDrawer : PropertyDrawer
{
    static Dictionary<string, Type> typeMap;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if(typeMap == null) BuildTypeMap();
        
        //reserve some space for the type selector button at the top. The rest of space will be used to draw the actual property fields after a type is selected
        var typeRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        var contentRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, position.height - EditorGUIUtility.singleLineHeight);
        
        
        EditorGUI.BeginProperty(position, label, property);
        var typeName = property.managedReferenceFullTypename;
        var displayName = GetShortTypeName(typeName);
        
        //draw drop-down button
        if(EditorGUI.DropdownButton(typeRect, new GUIContent(displayName ?? "Select Effect Type"), FocusType.Keyboard))
        {
            var menu = new GenericMenu();
            if(typeMap == null || typeMap.Count == 0)
            {
                menu.AddDisabledItem(new GUIContent("No Skill Effects available"));
                menu.ShowAsContext();
                return;
            }
            
            //loop through all the effect types we found via reflection
            foreach(var kvp in typeMap)
            {
                var name = kvp.Key;
                var type = kvp.Value;
                menu.AddItem(new GUIContent(name), type.FullName == typeName, () => {
                    property.managedReferenceValue = Activator.CreateInstance(type);
                    property.serializedObject.ApplyModifiedProperties();
                });                
            }
            menu.ShowAsContext();                        
        }
        
        if(property.managedReferenceValue != null) //if a valid instance is selected
        {
            EditorGUI.indentLevel++;
            EditorGUI.PropertyField(contentRect, property, GUIContent.none, true);
            EditorGUI.indentLevel--;
        }
            
        EditorGUI.EndProperty();
    }
    
    /// <summary>
    /// return enough space for the drop down button
    /// </summary>
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true) + EditorGUIUtility.singleLineHeight;
    }
    
    static void BuildTypeMap()
    {
        var baseType = typeof(ISkill);
        typeMap = AppDomain.CurrentDomain.GetAssemblies() //scan all the assemblies currently loaded into the app domain.
            .SelectMany(asm => {
                try {return asm.GetTypes();}  //flatten each assembly into its own list of types
                catch{ return Type.EmptyTypes;}
                })
                .Where(t => !t.IsInterface && baseType.IsAssignableFrom(t)) //인터페이스 제외
                .ToDictionary(t => ObjectNames.NicifyVariableName(t.Name), t => t); //clean up the type name
    }
    
    /// <summary>
    /// extract the class name form Unity's mangled manage reference full type name
    /// </summary>
    static string GetShortTypeName(string fullTypeName)
    {
        if(string.IsNullOrEmpty(fullTypeName)) return null;
        var parts = fullTypeName.Split(' ');
        return parts.Length > 1 ? parts[1].Split('.').Last() : fullTypeName;        
    }
}
