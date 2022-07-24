using Assets.Scripts.Utilities;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class EnergyManager : MonoBehaviour
    {
        private const int BeginningEnergyValue = 100,
                          RefreshEnergyValue = 50;

        public static EnergyManager Instance;

        [UsedImplicitly]
        private void Awake()
        {
            Instance = this;
        }

        public bool Unlimited()
        {
            return GamePlayerPrefs.GetBool("UnlimitedEnergy");
        }

        public bool HasEnergy()
        {
            return GetEnergy() > 0 || Unlimited();
        }

        public int GetEnergy()
        {
            return GamePlayerPrefs.GetInt("Energy", BeginningEnergyValue);
        }

        public void RefreshEnergy()
        {
            GamePlayerPrefs.SetInt("Energy", RefreshEnergyValue);
        }

        [UsedImplicitly]
        public void RewardFromVideo()
        {
            var newAmount = GetEnergy() + 10;
            GamePlayerPrefs.SetInt("Energy", newAmount);
        }

        public void SetUnlimitedEnergy()
        {
            GamePlayerPrefs.SetBool("UnlimitedEnergy", true);
        }

        public void ReduceEnergy()
        {
            var newAmount = GetEnergy() - 1;
            GamePlayerPrefs.SetInt("Energy", newAmount);
        }
    }
}
