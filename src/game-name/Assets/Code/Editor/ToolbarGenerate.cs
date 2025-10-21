using System;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Code.Editor
{
	[InitializeOnLoad]
	public class ToolbarGenerate
	{
		private static GUIStyle textStyle;
		
		static ToolbarGenerate()
		{
			ToolbarExtender.RegisterLeftEntry(OnToolbarGUI, 0);
		}

		private static void OnToolbarGUI()
		{
			if (Application.isPlaying)
				return;
			
			if (textStyle == null)
			{
				textStyle = new  GUIStyle(EditorStyles.label)
				{
					alignment = TextAnchor.MiddleCenter,
					normal =
					{
						textColor = new Color(0.76f, 0.76f, 0.76f)
					},
					hover = 
					{
						textColor = Color.white
					}
				};
			}
			GUILayout.FlexibleSpace();

			if (GUILayout.Button("Generate code", EditorStyles.toolbarButton))
			{
				RunJenny();
			}
		}
		
		/// <summary>
        /// Calls the Jenny generator script.
        /// Adjust the 'scriptRelPathFromProjectRoot' if your layout differs.
        /// </summary>
        private static void RunJenny()
        {
	        string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
	        string scriptPath  = Path.Combine(projectRoot, "../../Jenny", "Jenny-Gen");

#if UNITY_EDITOR_WIN
			string batPath = Path.Combine(projectRoot, "../../Jenny", "Jenny-Gen.bat");
            string comspec = Environment.GetEnvironmentVariable("ComSpec") 
                             ?? @"C:\Windows\System32\cmd.exe";

            ExecuteProcess(
                comspec,
                $"/c \"{batPath}\"",
                projectRoot
            );
#else
	        ExecuteProcess(
		        "/bin/zsh",
		        $"-lc \"\\\"{scriptPath}\\\"\"",
		        projectRoot
	        );
#endif

        }

        private static void ExecuteProcess(string fileName, string arguments, string workingDirectory)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = arguments,
                    WorkingDirectory = workingDirectory,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

/*#if UNITY_EDITOR_WIN
                // If your .bat calls 'dotnet' and it's not on PATH for Unity, uncomment:
                // var current = Environment.GetEnvironmentVariable("PATH") ?? "";
                // psi.EnvironmentVariables["PATH"] = 
                //     (@"C:\Program Files\dotnet;" + current);
#endif*/

                using (var proc = Process.Start(psi))
                {
                    EditorUtility.DisplayProgressBar("Jenny", "Generating…", 0.5f);

                    if (proc == null)
                        throw new Exception("Failed to start process.");

                    string stdout = proc.StandardOutput.ReadToEnd();
                    string stderr = proc.StandardError.ReadToEnd();
                    proc.WaitForExit();

                    EditorUtility.ClearProgressBar();

                    if (!string.IsNullOrEmpty(stdout))
                        Debug.Log($"[Jenny] {stdout}");
                    if (!string.IsNullOrEmpty(stderr))
                        Debug.LogError($"[Jenny] {stderr}");

                    if (proc.ExitCode != 0)
                    {
                        Debug.LogError($"[Jenny] Exited with code {proc.ExitCode}");
                    }
                    else
                    {
                        AssetDatabase.Refresh();
                        Debug.Log("[Jenny] Generation completed.");
                    }
                }
            }
            catch (Exception ex)
            {
                EditorUtility.ClearProgressBar();
                Debug.LogError($"[Jenny] Failed to run: {ex.Message}");
            }
        }
	}
}