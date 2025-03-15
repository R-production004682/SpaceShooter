using UnityEditor;
using UnityEngine;

public class BuildScript
{
    /// <summary>
    /// Jenkins����r���h�����s���邽�߂̃��\�b�h
    /// </summary>
    public static void PerformBuild()
    {
        string[] scenes = { "Assets/Scenes/GameScene.unity" }; 
        string buildPath = "Build/SpaceShooter.exe";

        // �r���h�^�[�Q�b�g��ݒ�
        BuildTarget target = BuildTarget.StandaloneWindows64;

        // �r���h�����s
        BuildPipeline.BuildPlayer(scenes, buildPath, target, BuildOptions.None);
    }
}
