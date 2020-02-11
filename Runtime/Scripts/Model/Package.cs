namespace PackageCreatorEditorTool
{
    using System.Collections.Generic;
    [System.Serializable]
    public struct AutherDetails
    {
        public string name;
        public string email;
        public string url;

        public void Reset()
        {
            name = string.Empty;
            email = string.Empty;
            url = string.Empty;
        }
    }
    [System.Serializable]
    public struct RepositoryDetails
    {
        public enum RepositoryType
        {
            None,
            Git,
            Disk
        }

        public string type;
        public string url;
        public void Reset()
        {
            type = string.Empty;
            url = string.Empty;
        }
    }
    [System.Serializable]

    public struct DependencyDetails
    {
    }
    [System.Serializable]
    public struct Package
    {
        public string name;
        public string displayName;
        public string description;
        public List<string> keywords;
        public string version;
        public string unity;
        public AutherDetails author;
        public RepositoryDetails repository;
        public void Reset()
        {
            name = string.Empty;
            displayName = string.Empty;
            description = string.Empty;
            //keywords.Clear();
            version = string.Empty;
            unity = string.Empty;

            author.Reset();
            repository.Reset();
        }
    }
}