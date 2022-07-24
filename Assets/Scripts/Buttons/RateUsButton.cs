using UnityEngine;

namespace Assets.Scripts.Buttons
{
    public class RateUsButton : AnimatedButton
    {
        protected override void OnClick()
        {
            base.OnClick();
            Application.OpenURL("https://play.google.com/store/apps/details?id=com.Toughwin.ninetyNineSlides");
        }
    }
}
