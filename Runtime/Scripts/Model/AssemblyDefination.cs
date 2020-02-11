namespace PackageCreatorEditorTool
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    [System.Serializable]
    public struct AssemblyDefination
    {
        public string name;
        public List<string> references;
        public List<string> optionalUnityReferences;
        public List<string> includePlatforms;
        public List<string> excludePlatforms;
        public bool allowUnsafeCode;
        public bool overrideReferences;
        public List<string> precompiledReferences;
        public bool autoReferenced;
        public List<string> defineConstraints;
        public List<string> versionDefines;
    }
}
