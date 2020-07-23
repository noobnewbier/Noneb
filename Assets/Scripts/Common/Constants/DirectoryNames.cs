namespace Common.Constants
{
    public static class DirectoryNames
    {
#if UNITY_EDITOR
        public static string Environments => "Environments";
#else
        public static string Environments => Application.persistentDataPath + "//Environments";
#endif
    }
}