using Assets.Scripts.Managers;
using Assets.Scripts.Managers.Audio;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Buttons
{
    public class AnimatedButton : AbstractButton
    {
        private bool isLocked;

        private void ToggleButtonAlpha(bool reduce)
        {
            var currentColor = SpriteRend.color;
            currentColor.a = (reduce ? currentColor.a / 1.5f : 1);
            SpriteRend.color = currentColor;
        }

        [UsedImplicitly]
        protected virtual void OnnMouseDown()
        {
            if (isLocked || !ButtonsActive) return;
            Tr.localScale *= 1.1f;
        }

        [UsedImplicitly]
        protected virtual void OnnMouseUp()
        {
            if (isLocked || !ButtonsActive) return;
            Tr.localScale = Vector2.one;

            if (ReleasedOverButton())
                OnClick();                
        }

        protected virtual void OnClick()
        {
            EffectsAudioManager.Instance.PlayClick();
        }

        private bool ReleasedOverButton()
        {
            return SortingLayerManager.IsTopmost(Go);
        }

        public void Reset()
        {
            isLocked = false;
            Tr.localScale = Vector2.one;
            SpriteRend.color = Color.white;
        }

        public virtual void Lock()
        {
            isLocked = true;
            ToggleButtonAlpha(true);
        }

        public virtual void Unlock()
        {
            isLocked = false;
            ToggleButtonAlpha(false);
        }
    }
}
