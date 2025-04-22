using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace Constant
{
    /// <summary>
    /// ��ʊO�ɍs���Ȃ��悤�Ɏl���ɐ�����������B
    /// </summary>
    public static class LimitedPosition
    {
        public const float SPAWN_TOP = 9.5f;
        public const float TOP = 0.0f;
        public const float BOTTOM = -3.8f;
        public const float LEFT = -11.0f;
        public const float RIGHT = 11.0f;
    }

    /// <summary>
    /// �Q�[���v���C���ɍŏ��ɐ�������I�u�W�F�N�g
    /// </summary>
    public static class StartGenerateActorInfo
    {
        public const float POSITION = 100.0f;
    }

    /// <summary>
    /// ��������܂ł̎���
    /// </summary>
    public static class SpawnObjectTime
    {
        public const float ENEMY = 5.0f;
        public const float BOOSTERS_ITEM = 3.0f;
        public const float SHIELD_ITEM = 4.0f;
        public const float SPEEDUP_ITEM = 4.0f;
    }

    /// <summary>
    /// �A�C�e���̌��ʎ���
    /// </summary>
    public static class ItemEffectInfo
    {
        public const float BOOSTERS_ITEM_ENABLE_TIME = 6.0f;
    }
}
