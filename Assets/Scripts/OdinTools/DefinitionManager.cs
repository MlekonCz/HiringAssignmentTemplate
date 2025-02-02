﻿using System.Collections.Generic;
using System.Linq;
using Definitions;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;


    public enum ManagerState
    {
        // 1. Add new field that will be used as a Tab
        Equations,
        Levels
    }

    public class DefinitionManager : OdinMenuEditorWindow
    {
        [OnValueChanged("StateChange")]
        [LabelText("Manager View")]
        [LabelWidth(100f)]
        [EnumToggleButtons]
        [ShowInInspector]
        private ManagerState _managerState;

        private bool _treeRebuild = false;
        private int _enumIndex = 0;

        // 2. Declare definition that you want to display in Tab
        // If you will want more folders in one Tab like in Weapons have Melee and Ranged 
        // You have to declare it here for each new folder that will be in Tab to be able to select path for it
        private readonly DrawSelected<EquationDefinition> _drawEasyEquations = new DrawSelected<EquationDefinition>();
        private readonly DrawSelected<EquationDefinition> _drawMediumEquations = new DrawSelected<EquationDefinition>();
        private readonly DrawSelected<LevelDefinition> _drawLevels = new DrawSelected<LevelDefinition>();

        
        // 3. Declare path for each folder you will want to be displayed in which are SO 
        
        private const string easyEquationDefinitionPath = "Assets/Data/Definitions/Equations/Easy";
        private const string mediumEquationDefinitionPath = "Assets/Data/Definitions/Equations/Medium";
        private const string levelDefinitionPath = "Assets/Data/Definitions/Levels";
        
        

        [MenuItem("Tools/Definition Manager &#D")]
        public static void OpenWindow()
        {
            GetWindow<DefinitionManager>().Show();
        }

        private void StateChange()
        {
            _treeRebuild = true;
        }

        // 4. Setting path to definition
        protected override void Initialize()
        {
            _drawEasyEquations.SetPath(easyEquationDefinitionPath);
            _drawEasyEquations.SetPath(mediumEquationDefinitionPath);
            _drawLevels.SetPath(levelDefinitionPath);
        }

        protected override void OnGUI()
        {
            if (_treeRebuild && Event.current.type == EventType.Layout)
            {
                ForceMenuTreeRebuild();
                _treeRebuild = false;
            }

            SirenixEditorGUI.Title("Definition Manager", "Tool for managing scriptable objects", TextAlignment.Left,
                true);
            EditorGUILayout.Space();
            switch (_managerState)
            {
                // 5. Here you add enum so it will create new Tab in Definition Manager
                case ManagerState.Equations:
                case ManagerState.Levels: 
                    DrawEditor(_enumIndex);
                    break;
                default:
                    break;
            }
            EditorGUILayout.Space();
            base.OnGUI();
        }

        protected override void DrawEditors()
        {
            switch (_managerState)
            {
                // 6. For each Tab you will have you need to add case so system will be able to update when selected new Tab
                case ManagerState.Equations:
                    _drawEasyEquations.SetSelected(MenuTree.Selection.SelectedValue);
                    break;
                case ManagerState.Levels:
                    _drawLevels.SetSelected(MenuTree.Selection.SelectedValue);
                    break;
                default:
                    break;
            }

            DrawEditor((int) _managerState);
        }

        protected override IEnumerable<object> GetTargets()
        {
            // 7. Adding definitions to the enums in the list
            // THEY HAS TO BE IN SAME ORDERS AS ENUMS THAT ARE ON TOP!
            // in case you are not using some enum Then you just write: targets.Add(null);
            List<object> targets = new List<object>();
            targets.Add(_drawEasyEquations);
            targets.Add(_drawLevels);
            targets.Add(base.GetTarget());

            _enumIndex = targets.Count - 1;
            return targets;
        }

        protected override void DrawMenu()
        {
            switch (_managerState)
            {
                // 8. Add here State that you want to add
                case ManagerState.Equations:
                case ManagerState.Levels:
                    base.DrawMenu();
                    break;
                default:
                    break;
            }
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree tree = new OdinMenuTree();
            switch (_managerState)
            {
                // 9. Here you add folders to the Tabs that you want to be displayed
                // If you want to add multiple folders to one Tab you just need to 
                // write tree.AddAllAssetsAtPath(...) 
                //      typeof(...)
                // for each folder under case which you want to add it to
                case ManagerState.Equations:
                    tree.AddAllAssetsAtPath("Easy Equation Definitions", easyEquationDefinitionPath, 
                        typeof(EquationDefinition));
                    tree.AddAllAssetsAtPath("Medium Equation Definitions", mediumEquationDefinitionPath, 
                        typeof(EquationDefinition));
                    break;
                case ManagerState.Levels:
                    tree.AddAllAssetsAtPath("Level Definition", levelDefinitionPath,
                        typeof(LevelDefinition));
                    break;
                default:
                    break;
            }
            return tree;
        }
    }

    public class DrawSelected<T> where T : ScriptableObject
    {
        [InlineEditor(InlineEditorObjectFieldModes.CompletelyHidden)]
        public T selected;

        [LabelWidth(100)] [PropertyOrder(-1)] [HorizontalGroup("Horizontal")]
        public string nameForNew;

        private string path;
        
        private bool deleteCheck = false;
        
        [HorizontalGroup("Horizontal")]
        [GUIColor(0.7f, 1f, 0.5f)]
        [Button(ButtonSizes.Medium)]
        public void CreateNew()
        {
            if (string.IsNullOrWhiteSpace(nameForNew))
            {
                return;
            }

            List<T> instancesInFolder = new List<T>();
            
            instancesInFolder = AssetDatabase.FindAssets("", new string[] {path})
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<T>).ToList();

            foreach (var scriptableObject in instancesInFolder)
            {
                if (scriptableObject.name == nameForNew)
                {
                    UnityEngine.Debug.Log("Name already used!");
                    return;
                }
            }
            
            T newItem = ScriptableObject.CreateInstance<T>();
            // newItem.name = "New " + typeof(T).ToString();

            if (string.IsNullOrWhiteSpace(path))
            {
                path = "Assets/";
            }

            AssetDatabase.CreateAsset(newItem, path + "\\" + nameForNew + ".asset");
            AssetDatabase.SaveAssets();
            nameForNew = "";
        }
        [HorizontalGroup("Horizontal")]
        [GUIColor(0.4f, 0.8f, 1f)]
        [Button(ButtonSizes.Medium)]
        public void CopySelected()
        {
            if (string.IsNullOrWhiteSpace(nameForNew))
            {
                return;
            }
            
            List<T> instancesInFolder = new List<T>();
            
            instancesInFolder = AssetDatabase.FindAssets("", new string[] {path})
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<T>).ToList();

            foreach (var scriptableObject in instancesInFolder)
            {
                if (scriptableObject.name == nameForNew)
                {
                    UnityEngine.Debug.Log("Name already used!");
                    return;
                }
            }
            
            T clonedScriptableObject = ScriptableObject.Instantiate(original:selected) as T;
            
            if (string.IsNullOrWhiteSpace(path))
            {
                path = "Assets/";
            }

            AssetDatabase.CreateAsset(clonedScriptableObject, path + "\\" + nameForNew + ".asset");
            AssetDatabase.SaveAssets();
            nameForNew = "";
        }

        [HideIf("deleteCheck",true)]
        [HorizontalGroup("Horizontal")]
        [GUIColor(1f, 0f, 0f)]
        [Button]
        public void DeleteSelected()
        {
            deleteCheck = true;
        }
        [ShowIf("deleteCheck",true)]
        [HorizontalGroup("Horizontal")]
        [GUIColor(0.7f, 1f, 0.5f)]
        [Button(ButtonSizes.Small)]
        public void No()
        {
            deleteCheck = false;
        }
        
        [ShowIf("deleteCheck",true)]
        [HorizontalGroup("Horizontal")]
        [GUIColor(1f, 0f, 0f)]
        [Button(ButtonSizes.Small)]
        public void Yes()
        {
            deleteCheck = false;
            if (selected != null)
            {
                string _path = AssetDatabase.GetAssetPath(selected);
                AssetDatabase.DeleteAsset(_path);
                AssetDatabase.SaveAssets();
            }
        }
       

        public void SetSelected(object item)
        {
            if (selected != item)
            {
                deleteCheck = false;
            }
            var attempt = item as T;
            if (attempt != null)
            {
                this.selected = attempt;
            }
        }

        public void SetPath(string path)
        {
            this.path = path;
        }
    }