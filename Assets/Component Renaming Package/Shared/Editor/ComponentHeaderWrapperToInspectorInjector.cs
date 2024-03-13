using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static Sisus.Shared.EditorOnly.InspectorContents;

namespace Sisus.Shared.EditorOnly
{
	[InitializeOnLoad]
	internal static class ComponentHeaderWrapperToInspectorInjector
	{
		static ComponentHeaderWrapperToInspectorInjector()
		{
			Editor.finishedDefaultHeaderGUI -= AfterInspectorRootEditorHeaderGUI;
			Editor.finishedDefaultHeaderGUI += AfterInspectorRootEditorHeaderGUI;
		}

		private static void AfterInspectorRootEditorHeaderGUI(Editor editor)
		{
			if(editor.target is GameObject)
			{
				// Handle InspectorWindow
				AfterGameObjectHeaderGUI(editor);
				return;
			}
			
			if(editor.target is Component)
			{
				// Handle PropertyEditor window opened via "Properties..." context menu item
				AfterComponentPropertiesHeaderGUI(editor);
			}
		}

		private static void AfterGameObjectHeaderGUI([DisallowNull] Editor gameObjectEditor)
		{
			foreach((Editor editor, IMGUIContainer header) editorAndHeader in GetComponentHeaderElementsFromEditorWindowOf(gameObjectEditor))
			{
				var onGUIHandler = editorAndHeader.header.onGUIHandler;
				if(onGUIHandler.Method is MethodInfo onGUI && onGUI.Name == nameof(ComponentHeaderWrapper.DrawWrappedHeaderGUI))
				{
					continue;
				}

				var component = editorAndHeader.editor.target as Component;
				var componentHeaderWrapper = new ComponentHeaderWrapper(editorAndHeader.header, component, true);
				editorAndHeader.header.onGUIHandler = componentHeaderWrapper.DrawWrappedHeaderGUI;
			}
		}

		private static void AfterComponentPropertiesHeaderGUI([DisallowNull] Editor componentEditor)
		{
			if(!(GetComponentHeaderElementFromPropertyEditorOf(componentEditor) is (Editor editor, IMGUIContainer header)))
			{
				return;
			}
			
			var onGUIHandler = header.onGUIHandler;
			if(onGUIHandler.Method is MethodInfo onGUI && onGUI.Name == nameof(ComponentHeaderWrapper.DrawWrappedHeaderGUI))
			{
				return;
			}

			var component = editor.target as Component;
			var componentHeaderWrapper = new ComponentHeaderWrapper(header, component, false);
			header.onGUIHandler = componentHeaderWrapper.DrawWrappedHeaderGUI;
		}
	}
}