namespace PackageCreatorEditorTool
{
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;
    using System.IO;

    [InitializeOnLoad]
    public class PackageCreator : EditorWindow
    {
        #region PRIVATE_VARS
        PackageCreatorEditorTool.Package package;
        PackageCreatorEditorTool.AssemblyDefination defination;
        DefaultAsset folder;
        DefaultAsset tempFolder;
        bool isPackageCreated;
        #endregion

        #region Unity_Callbacks
        [MenuItem("PackageCreator/Create")]
        public static void Initialize()
        {
            PackageCreator window = (PackageCreator)EditorWindow.GetWindow(typeof(PackageCreator));
            window.Show();
        }
        private void OnEnable()
        {
            isPackageCreated = false;
        }
        void OnGUI()
        {
            DrawEditor();
        }
        void DrawEditor()
        {
            EditorUIUtility.DrawHorizontalLayout(() => DrawFolderSelector(), GUI.skin.GetStyle("helpBox"));
            if (folder)
            {
                EditorUIUtility.DrawVerticalLayout(() => DrawPackageDetails());
                EditorUIUtility.DrawVerticalLayout(() => DrawAutherDetails());
                EditorUIUtility.DrawVerticalLayout(() => DrawRepositoryDetails());
                EditorUIUtility.DrawButton(PackageEditorConstants.create, () => OnCreateButtonClick());
                if(isPackageCreated)
                {
                    EditorGUILayout.BeginVertical(GUI.skin.GetStyle("helpBox"));
                    EditorUIUtility.DrawLabel("Package Created Successfully");
                    EditorUIUtility.DrawLabel("Move your files to runtime and editor folder accordingly.");
                    EditorGUILayout.EndVertical();
                }
            }
        }
        #endregion

        #region DrawFolderSelection
        public void DrawFolderSelector()
        {
            EditorUIUtility.DrawLabel(PackageEditorConstants.selectFolder, GUILayout.Width(PackageEditorConstants.i125));
            tempFolder = (DefaultAsset)EditorUIUtility.DrawObjectField(tempFolder, typeof(DefaultAsset));
            if(tempFolder!=folder)
            {
                Debug.Log("Called : DrawFolderSelector");
                folder = tempFolder;
                package = JsonCreator.LoadJson(AssetDatabase.GetAssetPath(folder));
            }
        }
        #endregion

        #region DrawPackageDetails
        public void DrawPackageDetails()
        {
            EditorGUILayout.BeginVertical(GUI.skin.GetStyle("helpBox"));
            GUILayout.Space(10);
            EditorUIUtility.DrawLabel("Package Details ", 20, TextAnchor.MiddleCenter, null);
            GUILayout.Space(10);
            EditorUIUtility.DrawHorizontalLayout(() => DrawPackageName());
            EditorUIUtility.DrawHorizontalLayout(() => DrawVersionCode());
            EditorUIUtility.DrawHorizontalLayout(() => DrawDisplayName());
            EditorUIUtility.DrawHorizontalLayout(() => DrawDescription());
            EditorUIUtility.DrawHorizontalLayout(() => DrawUnityVersion());
            EditorUIUtility.DrawHorizontalLayout(() => DrawKeywords());
            EditorGUILayout.EndVertical();
        }
        public void DrawPackageName()
        {
            EditorUIUtility.DrawLabel(PackageEditorConstants.packageName, GUILayout.Width(PackageEditorConstants.i125));
            package.name = EditorUIUtility.DrawTextField(package.name);
        }

        public void DrawVersionCode()
        {
            EditorUIUtility.DrawLabel(PackageEditorConstants.version, GUILayout.Width(PackageEditorConstants.i125));
            package.version = EditorUIUtility.DrawTextField(package.version);
        }
        public void DrawUnityVersion()
        {
            EditorUIUtility.DrawLabel(PackageEditorConstants.unityVersion, GUILayout.Width(PackageEditorConstants.i125));
            package.unity = EditorUIUtility.DrawTextField(package.unity);
        }
        public void DrawKeywords()
        {
            EditorUIUtility.DrawLabel(PackageEditorConstants.keyWords, GUILayout.Width(PackageEditorConstants.i125));
            EditorGUILayout.BeginVertical();
            DrawKeyword();
            EditorUIUtility.DrawButton(PackageEditorConstants.add, () => OnAddButtonClicked());
            EditorGUILayout.EndVertical();
        }

        public void DrawKeyword()
        {
            if (package.keywords == null)
            {
                package.keywords = new List<string>();
            }
            for (int indexOfKeyword = package.keywords.Count - 1; indexOfKeyword >= 0; indexOfKeyword--)
            {
                EditorGUILayout.BeginHorizontal();
                package.keywords[indexOfKeyword] = EditorUIUtility.DrawTextField(package.keywords[indexOfKeyword]);
                EditorUIUtility.DrawButton(PackageEditorConstants.remove, () => OnRemoveButtonClick(indexOfKeyword));
                EditorGUILayout.EndHorizontal();
            }
        }

        public void OnAddButtonClicked()
        {
            package.keywords.Insert(0, string.Empty);
        }

        public void OnRemoveButtonClick(int index)
        {
            package.keywords.RemoveAt(index);
        }

        public void DrawDisplayName()
        {
            EditorUIUtility.DrawLabel(PackageEditorConstants.displayName, GUILayout.Width(PackageEditorConstants.i125));
            package.displayName = EditorUIUtility.DrawTextField(package.displayName);
        }
        public void DrawDescription()
        {
            EditorUIUtility.DrawLabel(PackageEditorConstants.description, GUILayout.Width(PackageEditorConstants.i125));
            package.description = EditorUIUtility.DrawTextArea(package.description, GUILayout.Height(100));
        }
        #endregion

        #region DrawAutherDetails
        public void DrawAutherDetails()
        {
            EditorGUILayout.BeginVertical(GUI.skin.GetStyle("helpBox"));
            GUILayout.Space(10);
            EditorUIUtility.DrawLabel("Auther Details ", 20, TextAnchor.MiddleCenter, null);
            GUILayout.Space(10);
            EditorUIUtility.DrawHorizontalLayout(() => DrawAutherName());
            EditorUIUtility.DrawHorizontalLayout(() => DrawAutherEmail());
            EditorUIUtility.DrawHorizontalLayout(() => DrawAutherUrl());

            EditorGUILayout.EndVertical();
        }
        public void DrawAutherName()
        {
            EditorUIUtility.DrawLabel(PackageEditorConstants.autherName, GUILayout.Width(PackageEditorConstants.i125));
            package.author.name = EditorUIUtility.DrawTextField(package.author.name);
        }
        public void DrawAutherEmail()
        {
            EditorUIUtility.DrawLabel(PackageEditorConstants.autherEmail, GUILayout.Width(PackageEditorConstants.i125));
            package.author.email = EditorUIUtility.DrawTextField(package.author.email);
        }
        public void DrawAutherUrl()
        {
            EditorUIUtility.DrawLabel(PackageEditorConstants.url, GUILayout.Width(PackageEditorConstants.i125));
            package.author.url = EditorUIUtility.DrawTextField(package.author.url);
        }
        #endregion

        #region DrawRepoDetails
        public void DrawRepositoryDetails()
        {
            EditorGUILayout.BeginVertical(GUI.skin.GetStyle("helpBox"));
            GUILayout.Space(10);
            EditorUIUtility.DrawLabel("Repository Details ", 20, TextAnchor.MiddleCenter, null);
            GUILayout.Space(10);
            EditorUIUtility.DrawHorizontalLayout(() => DrawRepoType());
            EditorUIUtility.DrawHorizontalLayout(() => DrawUrl());
            EditorGUILayout.EndVertical();
        }
        int index;
        public void DrawRepoType()
        {
            EditorUIUtility.DrawLabel(PackageEditorConstants.type, GUILayout.Width(PackageEditorConstants.i125));
            index = EditorGUILayout.Popup(index, PackageEditorConstants.repoType);
            package.repository.type = PackageEditorConstants.repoType[index];
        }
        public void DrawUrl()
        {
            EditorUIUtility.DrawLabel(PackageEditorConstants.url, GUILayout.Width(PackageEditorConstants.i125));
            package.repository.url = EditorUIUtility.DrawTextField(package.repository.url);
        }
        #endregion

        #region PackageCreator

        public void OnCreateButtonClick()
        {
            CreateFolders();
            CreateJasonFile();
            CreateAssemblyDefinationFile();
            isPackageCreated = true;
        }
        public void CreateFolders()
        {
            string editorFolderPath = AssetDatabase.GetAssetPath(folder) + "/Editor";
            string runtimeFolderPath = AssetDatabase.GetAssetPath(folder) + "/Runtime";
            if (!Directory.Exists(editorFolderPath))
            {
                Directory.CreateDirectory(editorFolderPath);
            }
            if (!Directory.Exists(runtimeFolderPath))
            {
                Directory.CreateDirectory(runtimeFolderPath);
            }
           
        }
        public void CreateAssemblyDefinationFile()
        {
            string runtimeFolderPath = AssetDatabase.GetAssetPath(folder) ;

            //Assign name
            defination.name = package.name;
            defination.autoReferenced = true;

            //Create File
            JsonCreator.SaveAssembly(defination,runtimeFolderPath);
            Debug.Log("CreateAssemblyDefinationFile");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        public void CreateJasonFile()
        {
            JsonCreator.SaveJson(package, AssetDatabase.GetAssetPath(folder));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        #endregion
    }
}
