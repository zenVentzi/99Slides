using Assets.Scripts.Buttons;
using Assets.Scripts.Utilities;

namespace Assets.Scripts
{
    public class PlaySlide : MyMono
    {
        public void Activate()
        {
            var purchaseBtn = Tr.GetChild(0).gameObject;
            var playBtn = Tr.GetChild(1).gameObject;
            var time = Tr.GetChild(2).gameObject;

            purchaseBtn.SetActive(false);
            playBtn.GetComponent<PlayButton>().Unlock();
            time.SetActive(false);
            GamePlayerPrefs.DeleteKey(CooldownTime.CooldownLaunchTimeKey);
        }
    }
}
