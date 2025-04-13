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
        public const float LEFT = -11.0f;
        public const float RIGHT = 11.0f;
    }

    public static class ReSpawn
    {
        public const float TIME = 5.0f;
    }
}
