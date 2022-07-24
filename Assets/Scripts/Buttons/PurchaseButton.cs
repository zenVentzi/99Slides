using System;
using Assets.Scripts.Managers;
using Assets.Scripts.Plugins;
using JetBrains.Annotations;
using Soomla.Store;
using UnityEngine;

namespace Assets.Scripts.Buttons
{
    public class PurchaseButton : AnimatedButton
    {
        [UsedImplicitly]
        private void Awake()
        {
            if (EnergyManager.Instance.Unlimited() || EnergyManager.Instance.HasEnergy())
            {
                Go.SetActive(false);
            }
        }

        [UsedImplicitly]
        private void OnEnable()
        {
            SubscribeSoomlaEvents();            
        }

        [UsedImplicitly]
        private void OnDisable()
        {
            UnscribeSoomlaEvents();
        }

        #region event methods
        private void SubscribeSoomlaEvents()
        {
            StoreEvents.OnItemPurchased += OnItemPurchased;
            StoreEvents.OnItemPurchaseStarted += OnItemPurchaseStarted;
            StoreEvents.OnUnexpectedErrorInStore += OnUnexpectedErrorInStore;
            StoreEvents.OnMarketPurchaseCancelled += OnMarketPurchaseCancelled;
        }

        private void UnscribeSoomlaEvents()
        {
            StoreEvents.OnItemPurchased -= OnItemPurchased;
            StoreEvents.OnItemPurchaseStarted -= OnItemPurchaseStarted;
            StoreEvents.OnUnexpectedErrorInStore -= OnUnexpectedErrorInStore;
            StoreEvents.OnMarketPurchaseCancelled -= OnMarketPurchaseCancelled;
        }

        private void OnMarketPurchaseCancelled(PurchasableVirtualItem purchasableVirtualItem)
        {
            //Debug.Log("SOOMLA OnMarketPurchaseCancelledd Enzi 1.0");
            ActivateButtons();
        }

        private void OnUnexpectedErrorInStore(string error)
        {
            //Debug.Log("SOOMLA OnUnexpectedErrorInStore Enzi 1.0  " + error);
            ActivateButtons();
        }

        private void OnItemPurchaseStarted(PurchasableVirtualItem purchasableVirtualItem)
        {
            //Debug.Log("SOOMLA OnItemPurchaseStarted Enzi 1.0");
            DeactivateButtons();
        }

        private void OnItemPurchased(PurchasableVirtualItem arg1, string payload)
        {
            EnergyManager.Instance.SetUnlimitedEnergy();
            ActivateButtons();

            var playSlide = FindObjectOfType<PlaySlide>();

            if (playSlide != null)
                playSlide.Activate();
        }
        #endregion

        protected override void OnClick()
        {
            base.OnClick();
            StoreInventory.BuyItem(NinetyNineSlidesAssetStore.FullGameId, "Purchase finished successfully!");
        }
    }
}
