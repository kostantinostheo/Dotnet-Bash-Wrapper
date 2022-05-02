using UnityEditor;
using UnityEngine;
using UnityBashSystem;

public class SampleEditorWindow : EditorWindow
{
    GUIStyle font = new GUIStyle();
    GUIStyle headerMid = new GUIStyle();
    GUIStyle header = new GUIStyle();
    GUIStyle pathFont = new GUIStyle();

    private string bashFile;
    private string gitBash;
    private bool canExecute;
    string text;
    // Add menu named "My Window" to the Window menu
    [MenuItem("Unity Tools/Execute Bash Files")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        SampleEditorWindow window = (SampleEditorWindow)EditorWindow.GetWindow(typeof(SampleEditorWindow));
        window.Show();
    }

    void OnGUI()
    {
        font.margin.left = 10;
        font.normal.textColor = Color.white;

        pathFont.margin.left = 10;
        pathFont.normal.textColor = Color.green;

        header.margin.left = 10;
        header.normal.textColor = Color.white;
        header.fontSize = 16;
        header.fontStyle = FontStyle.Bold;

        headerMid.margin.left = 10;
        headerMid.normal.textColor = Color.white;
        headerMid.fontSize = 14;
        headerMid.fontStyle = FontStyle.Bold;

        #region Windows Region
        EditorGUI.BeginDisabledGroup(Application.platform != RuntimePlatform.WindowsEditor);
        GUILayout.Space(10);
        GUILayout.Label("Windows Region", header);
        GUILayout.Space(5);
        BrowseGitBash("Path to Git-Bash.exe: (Windows Only)", "Path to Git - Bash.exe");
        EditorGUI.EndDisabledGroup();
        #endregion

        GUILayout.Space(20);
        GUILayout.Label("What You Can Do", header);
        GUILayout.Space(5);
        GUILayout.Label("1. Execute Bash Files (.sh)", headerMid);
        GUILayout.Space(5);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Bash File Path: ", font);
        GUI.skin.button.alignment = TextAnchor.MiddleCenter;
        if (GUILayout.Button("Browse", GUILayout.Width(150)))
            bashFile = EditorUtility.OpenFilePanel("Your Bash File", "", "");
        GUILayout.EndHorizontal();
        GUILayout.Space(5);
        GUILayout.Label(string.IsNullOrEmpty(bashFile) ? "-" : bashFile, pathFont);

#if UNITY_EDITOR_WIN
        EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(bashFile) || string.IsNullOrEmpty(gitBash));
#elif UNITY_EDITOR_OSX
        EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(bashFile));
#endif
        GUILayout.Space(10);
        if (GUILayout.Button("Make Executable with 'shmod'"))
        {
            string fileName = "bash.sh";
            string fileLocation = Application.dataPath + "/Editor";
#if UNITY_EDITOR_WIN
            canExecute = UnityBashManager.Shmod(fileName, fileLocation, gitBash);
#elif UNITY_EDITOR_OSX
            canExecute = UnityBashManager.Shmod(fileName, fileLocation);
#endif
        }
        EditorGUI.BeginDisabledGroup(!canExecute);
        if (GUILayout.Button("Start Execution"))
        {
            string command = "./"+System.IO.Path.GetFileName(bashFile);
            string workingDir = System.IO.Path.GetDirectoryName(bashFile);

#if UNITY_EDITOR_WIN
            UnityBashManager.Run(command, workingDir, gitBash);
#elif UNITY_EDITOR_OSX
            UnityBashManager.BashExecute(command, workingDir);
#endif
        }
        EditorGUI.EndDisabledGroup();
        EditorGUI.EndDisabledGroup();


        #region LinuxCommand
        GUILayout.Space(5);
        GUILayout.Label("2. Execute Custom Unix Commands", headerMid);
        GUILayout.Space(5);
        
        text = EditorGUILayout.TextField("Linux Commnad: ", text);
        EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(text));
        
        if (GUILayout.Button("Execute"))
            UnityBashManager.Run(text, gitBash);

        EditorGUI.EndDisabledGroup();
        GUILayout.Space(5);
        GUILayout.Label("3. Have Access to File System", headerMid);
        GUILayout.Space(5);

        string path = Application.dataPath + "/Editor/Bash Manager for Unity/";
        if (GUILayout.Button("Execute 'ls' command"))
            UnityBashManager.ListFiles(path);

        #endregion
    }

    private void BrowseGitBash(string labelText, string browseTitle, string directory="", string fileExtension = "")
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(labelText, font);
        if (GUILayout.Button("Browse", GUILayout.Width(150)))
            gitBash = EditorUtility.OpenFilePanel(browseTitle, directory, fileExtension);
        GUILayout.EndHorizontal();
        GUILayout.Space(5);
        GUILayout.Label(string.IsNullOrEmpty(gitBash) ? "-" : gitBash, pathFont);
    }
    
}
