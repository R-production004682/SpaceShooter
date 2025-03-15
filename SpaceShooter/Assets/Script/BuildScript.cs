using UnityEditor;
using UnityEngine;

public class BuildScript
{
    /// <summary>
    /// Jenkinsからビルドを実行するためのメソッド
    /// </summary>
    public static void PerformBuild()
    {
        string[] scenes = { "Assets/Scenes/GameScene.unity" }; 
        string buildPath = "Build/SpaceShooter.exe";

        // ビルドターゲットを設定
        BuildTarget target = BuildTarget.StandaloneWindows64;

        // ビルドを実行
        BuildPipeline.BuildPlayer(scenes, buildPath, target, BuildOptions.None);
    }
}
