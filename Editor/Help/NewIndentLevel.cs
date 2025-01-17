#if UNITY_EDITOR

using System;
using UnityEditor;
namespace MJGame {

public class NewIndentLevel : IDisposable
{
    readonly int prevIndentLevel;

    public NewIndentLevel(int indentLevel)
    {
        prevIndentLevel = EditorGUI.indentLevel;
        EditorGUI.indentLevel = indentLevel;
    }

    void IDisposable.Dispose() => Dispose();
    public void Dispose()
    {
        EditorGUI.indentLevel = prevIndentLevel;
    }
}
}
#endif