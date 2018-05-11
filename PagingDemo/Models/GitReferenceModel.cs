namespace PagingDemo.Models
{
    class GitReferenceModel
    {
        public GitReferenceModel(string name, string label, string sha, string cloneUrl)
        {
            Name = name;
            Label = label;
            Sha = sha;
            CloneUrl = cloneUrl;
        }

        public string Name { get; }
        public string Label { get; }
        public string Sha { get; }
        public string CloneUrl { get; }
    }
}