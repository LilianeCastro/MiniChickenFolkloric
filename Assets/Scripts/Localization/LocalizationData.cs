[System.Serializable]
public class LocalizationData
{
    public LocalizationItem[] items;
    public LocalizationCute[] cutscene;
    public LocalizationCute[] gallery;

}

[System.Serializable]
public class LocalizationItem
{
    public string key;
    public string value;
}

[System.Serializable]
public class LocalizationCute
{
    public string key;
    public string value;
}

[System.Serializable]
public class LocalizationGallery
{
    public string key;
    public string value;
}
