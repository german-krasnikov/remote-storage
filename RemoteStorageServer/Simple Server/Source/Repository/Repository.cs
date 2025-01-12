using Newtonsoft.Json;

public sealed class Repository
{
    private readonly string _path;
    private readonly Dictionary<int, string> _gameVersions;

    public Repository(string path)
    {
        _path = path;

        if (!File.Exists(_path))
        {
            _gameVersions = new Dictionary<int, string>();
            return;
        }

        string content = File.ReadAllText(_path);
        if (string.IsNullOrEmpty(content))
        {
            _gameVersions = new Dictionary<int, string>();
            return;
        }

        _gameVersions = JsonConvert.DeserializeObject<Dictionary<int, string>>(content)
                        ?? new Dictionary<int, string>();
    }

    public void SetContent(int version, string state)
    {
        _gameVersions[version] = state;
        string json = JsonConvert.SerializeObject(_gameVersions);
        File.WriteAllText(_path, json);
    }

    public bool TryGetContent(int version, out string state)
    {
        return _gameVersions.TryGetValue(version, out state!);
    }
}