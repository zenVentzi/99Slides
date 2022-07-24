using Assets.Scripts.Managers.Audio;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Buttons
{
    public class SoundButton : AnimatedButton
    {
        [SerializeField, UsedImplicitly]
        private Sprite soundOnSprite;

        [SerializeField, UsedImplicitly]
        private Sprite soundOffSprite;

        [UsedImplicitly]
        private void Awake()
        {
            SpriteRend.sprite = AudioManager.Instance.SoundOn ? soundOnSprite : soundOffSprite;
        }

        protected override void OnClick()
        {
            base.OnClick();

            if (SpriteRend.sprite.name == soundOnSprite.name)
            {
                SpriteRend.sprite = soundOffSprite;
                AudioManager.Instance.Mute();
            }
            else
            {
                SpriteRend.sprite = soundOnSprite;
                AudioManager.Instance.Unmute();
            }
        }
    }
}
