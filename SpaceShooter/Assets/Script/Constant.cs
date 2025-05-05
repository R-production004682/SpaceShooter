using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace Constant
{
    /// <summary>
    /// 画面外に行かないように四方に制限をかける。
    /// </summary>
    public static class LimitedPosition
    {
        public const float SPAWN_TOP = 9.5f;
        public const float TOP = 0.0f;
        public const float BOTTOM = -3.8f;
        public const float LEFT = -12.0f;
        public const float RIGHT = 12.0f;
        public const float DEAD_LINE = -7.0f;
    }

    /// <summary>
    /// ゲームプレイ時に最初に生成するオブジェクト
    /// </summary>
    public static class StartGenerateActorInfo
    {
        public const float POSITION = 100.0f;
    }

    /// <summary>
    /// 復活するまでの時間
    /// </summary>
    public static class SpawnObjectTime
    {
        public const float ENEMY = 4.0f;
        public const float BOOSTERS_ITEM = 3.0f;
        public const float SHIELD_ITEM = 4.0f;
        public const float SPEEDUP_ITEM = 4.0f;
        public const float ASTRPID = 6.0f;
    }

    /// <summary>
    /// アイテムの効果
    /// </summary>
    public static class ItemEffectInfo
    {
        public const float BOOSTERS_ITEM_ENABLE_TIME = 4.0f;
    }

    /// <summary>
    /// 機体の損傷度
    /// </summary>
    public static class DamageLevel
    {
        public const int ALL_GREEN = 3;     // 良好
        public const int INSIGNIFICANT = 2; // 軽微
        public const int MEDIUM_DEGREE = 1; // 中程度
        public const int SERIOUS = 0;       // 重大
    }

    /// <summary>
    /// Sceneの名前を列挙する
    /// </summary>
    public static class SceneNames
    {
        public const string MAIN = "MainScene";
        public const string INGAME = "GameScene";
    }
}
