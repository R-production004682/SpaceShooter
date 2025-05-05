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
        public const float LEFT = -12.0f;
        public const float RIGHT = 12.0f;
        public const float DEAD_LINE = -7.0f;
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
        public const float ENEMY = 4.0f;
        public const float BOOSTERS_ITEM = 3.0f;
        public const float SHIELD_ITEM = 4.0f;
        public const float SPEEDUP_ITEM = 4.0f;
        public const float ASTRPID = 6.0f;
    }

    /// <summary>
    /// �A�C�e���̌���
    /// </summary>
    public static class ItemEffectInfo
    {
        public const float BOOSTERS_ITEM_ENABLE_TIME = 4.0f;
    }

    /// <summary>
    /// �@�̂̑����x
    /// </summary>
    public static class DamageLevel
    {
        public const int ALL_GREEN = 3;     // �ǍD
        public const int INSIGNIFICANT = 2; // �y��
        public const int MEDIUM_DEGREE = 1; // �����x
        public const int SERIOUS = 0;       // �d��
    }

    /// <summary>
    /// Scene�̖��O��񋓂���
    /// </summary>
    public static class SceneNames
    {
        public const string MAIN = "MainScene";
        public const string INGAME = "GameScene";
    }
}
