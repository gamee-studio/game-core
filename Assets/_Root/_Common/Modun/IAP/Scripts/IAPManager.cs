namespace Gamee.Hiuk.IAP
{   
    using System;
    using UnityEngine.Purchasing;
    using UnityEngine;
    using Gamee.Hiuk.Pattern;
    using Unity.Services.Core;
    using Unity.Services.Core.Environments;

    public class IAPManager : Singleton<IAPManager>, IStoreListener
    {
        public string environment = "production";

        private IStoreController myStoreController;
        private IExtensionProvider myExtensionProvider;

        private Action actionBuyRemoveAds;
        private Action actionBuyPack1;
        private Action actionBuyPack2;
        private Action actionBuyUnlockAllSkin;
        private Action actionBuyX2Coin;
        private Action actionBuyCombo;

        public async void Start()
        {
            if (myStoreController == null)
            {
                try
                {
                    var options = new InitializationOptions()
                        .SetEnvironmentName(environment);

                    await UnityServices.InitializeAsync(options);
                    Initialize();
                }
                catch (Exception ex)
                {
                    Debug.Log("Services initialization error " + ex.Message);
                }
            }
        }
        public void Initialize()
        {
            if (IsInitialized())
            {
                return;
            }

            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            builder.AddProduct(IAPData.REMOVE_ADS, ProductType.NonConsumable);
            builder.AddProduct(IAPData.PACK_1, ProductType.Consumable);
            builder.AddProduct(IAPData.PACK_2, ProductType.Consumable);
            builder.AddProduct(IAPData.UNLOCK_ALL_SKIN, ProductType.NonConsumable);
            builder.AddProduct(IAPData.X2_COIN, ProductType.NonConsumable);
            builder.AddProduct(IAPData.COMBO, ProductType.NonConsumable);
            UnityPurchasing.Initialize(this, builder);
        }

        public bool IsInitialized()
        {
            return myStoreController != null && myStoreController != null;
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Debug.Log("Initialized is Complete!");
            myStoreController = controller;
            myExtensionProvider = extensions;
        }

        public void BuyRemoveAds(Action actionRemoveAds)
        {
            this.actionBuyRemoveAds = actionRemoveAds;
            BuyProductID(IAPData.REMOVE_ADS);
        }

        public void BuyPack1(Action actionAddCoinPack1)
        {
            this.actionBuyPack1 = actionAddCoinPack1;
            BuyProductID(IAPData.PACK_1);
        }

        public void BuyPack2(Action actionAddCoinPack2)
        {
            this.actionBuyPack2 = actionAddCoinPack2;
            BuyProductID(IAPData.PACK_2);
        }

        public void BuyUnlockAllSkin(Action actionUnlockAllSkin)
        {
            this.actionBuyUnlockAllSkin = actionUnlockAllSkin;
            BuyProductID(IAPData.UNLOCK_ALL_SKIN);
        }

        public void BuyX2Coin(Action actionCoinX2)
        {
            this.actionBuyX2Coin = actionCoinX2;
            BuyProductID(IAPData.X2_COIN);
        }

        public void BuyCombo(Action actionBuyComboCompleted)
        {
            this.actionBuyCombo = actionBuyComboCompleted;
            BuyProductID(IAPData.COMBO);
        }
        void BuyProductID(string productId)
        {
            if (IsInitialized())
            {
                Product product = myStoreController.products.WithID(productId);

                if (product != null && product.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
#if UNITY_EDITOR
                    Purchase(productId);
#else
                    myStoreController.InitiatePurchase(product);
#endif
                }
                else
                {
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            else
            {
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
        }

        public void RestorePurchases()
        {
            if (!IsInitialized())
            {
                Debug.Log("RestorePurchases FAIL. Not initialized.");
                return;
            }

            if (Application.platform == RuntimePlatform.IPhonePlayer ||
                Application.platform == RuntimePlatform.OSXPlayer)
            {
                Debug.Log("RestorePurchases started ...");

                var apple = myExtensionProvider.GetExtension<IAppleExtensions>();
                apple.RestoreTransactions((result) =>
                {
                    Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
                });
            }
            else
            {
                Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
            }
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.Log("Initialized is Fail! " + error);
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            if (string.Equals(args.purchasedProduct.definition.id, IAPData.REMOVE_ADS, System.StringComparison.Ordinal))
            {
                IAPAdapter.BuyRemoveAds();
                actionBuyRemoveAds?.Invoke();
            }
            else if (string.Equals(args.purchasedProduct.definition.id, IAPData.PACK_1, System.StringComparison.Ordinal))
            {
                IAPAdapter.BuyPack1();
                actionBuyPack1?.Invoke();
            }
            else if (string.Equals(args.purchasedProduct.definition.id, IAPData.PACK_2, System.StringComparison.Ordinal))
            {
                IAPAdapter.BuyPack2();
                actionBuyPack2?.Invoke();
            }
            else if (string.Equals(args.purchasedProduct.definition.id, IAPData.UNLOCK_ALL_SKIN, System.StringComparison.Ordinal))
            {
                IAPAdapter.BuyUnlockAllSkin();
                actionBuyUnlockAllSkin?.Invoke();
            }
            else if (string.Equals(args.purchasedProduct.definition.id, IAPData.X2_COIN, System.StringComparison.Ordinal))
            {
                IAPAdapter.BuyX2Coin();
                actionBuyX2Coin?.Invoke();
            }
            else if (string.Equals(args.purchasedProduct.definition.id, IAPData.COMBO, System.StringComparison.Ordinal))
            {
                IAPAdapter.BuyCombo();
                actionBuyCombo?.Invoke();
            }

            return PurchaseProcessingResult.Complete;
        }

        public void Purchase(string productId) 
        {
            switch (productId) 
            {
                case IAPData.REMOVE_ADS:
                    IAPAdapter.BuyRemoveAds();
                    actionBuyRemoveAds?.Invoke();
                    break;
                case IAPData.PACK_1:
                    IAPAdapter.BuyPack1();
                    actionBuyPack1?.Invoke();
                    break;
                case IAPData.PACK_2:
                    IAPAdapter.BuyPack2();
                    actionBuyPack2?.Invoke();
                    break;
                case IAPData.UNLOCK_ALL_SKIN:
                    IAPAdapter.BuyUnlockAllSkin();
                    actionBuyUnlockAllSkin?.Invoke();
                    break;
                case IAPData.X2_COIN:
                    IAPAdapter.BuyX2Coin();
                    actionBuyX2Coin?.Invoke();
                    break;
                case IAPData.COMBO:
                    IAPAdapter.BuyCombo();
                    actionBuyCombo?.Invoke();
                    break;
            }
        }

        #region static api
        public static void BuyIAPRemoveAds(Action actionBuyRemoveAdsCompleted)
        {
            Instance.BuyRemoveAds(actionBuyRemoveAdsCompleted);
        }

        public void BuyIAPPack1(Action actionBuyPack1Completed)
        {
            Instance.BuyPack1(actionBuyPack1Completed);
        }

        public void BuyIAPPack2(Action actionBuyPack2Completed)
        {
            Instance.BuyPack2(actionBuyPack2Completed);
        }

        public void BuyIAPUnlockAllSkin(Action actionBuyUnlockAllSkinCompleted)
        {
            Instance.BuyUnlockAllSkin(actionBuyUnlockAllSkinCompleted);
        }

        public void BuyIAPX2Coin(Action actionbuyX2CoinCompleted)
        {
            Instance.BuyX2Coin(actionbuyX2CoinCompleted);
        }

        public void BuyIAPCombo(Action actionBuyComboCompleted)
        {
            Instance.BuyCombo(actionBuyComboCompleted);
        }
        #endregion
    }
}

