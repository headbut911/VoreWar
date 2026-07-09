#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
public class AssetBundlesPreBuilder : IPreprocessBuildWithReport
{
    public int callbackOrder { get { return 100; } }
    public void OnPreprocessBuild(BuildReport report)
    {
        if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            // Force the build to fail. This message will appear in the console and Editor log.
            throw new BuildFailedException("No builds are allowed on Thursdays");
    }
}
#endif