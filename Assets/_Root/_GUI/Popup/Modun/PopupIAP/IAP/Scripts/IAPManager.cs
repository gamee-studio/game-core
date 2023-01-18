namespace Gamee.Hiuk.IAP
{   
    using System;
    using UnityEngine.Purchasing;
    using UnityEngine;
    using Gamee.Hiuk.Pattern;

    public class IAPManager : Singleton<IAPManager>, IStoreListener
    {
        private IStoreController myStoreController;
        private IExtensionProvider myExtensionProvider;

        private Action actionBuyRemoveAds;
        private Action actionBuyPack1;
        private Action actionBuyPack2;
        private Action actionBuyUnlockAllSkin;
        private Action actionBuyX2Coin;
        private Action actionBuyCombo;

        private bool isBuyIap = false;

        public void Start()
        {
            if (myStoreController == null)
            {
                Initialize();
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
            isBuyIap = true;
            this.actionBuyRemoveAds = actionRemoveAds;
            BuyProductID(IAPData.REMOVE_ADS);
        }

        public void BuyAddCoinPack1(Action actionAddCoinPack1) 
        {
            isBuyIap = true;
            this.actionBuyPack1 = actionAddCoinPack1;
            BuyProductID(IAPData.PACK_1);
        }

        public void BuyAddCoinPack2(Action actionAddCoinPack2)
        {
            isBuyIap = true;
            this.actionBuyPack2 = actionAddCoinPack2;
            BuyProductID(IAPData.PACK_2);
        }

        public void BuyUnlockAllSkin(Action actionUnlockAllSkin) 
        {
            isBuyIap = true;
            this.actionBuyUnlockAllSkin = actionUnlockAllSkin;
            BuyProductID(IAPData.UNLOCK_ALL_SKIN);
        }

        public void BuyCoinX2(Action actionCoinX2)
        {
            isBuyIap = true;
            this.actionBuyX2Coin = actionCoinX2;
            BuyProductID(IAPData.X2_COIN);
        }

        public void BuyCombo(Action actionCombo)
        {
            isBuyIap = true;
            this.actionBuyCombo = actionCombo;
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

                    myStoreController.InitiatePurchase(product);
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
                apple.RestoreTransactions((result) => {
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
            if(string.Equals(args.purchasedProduct.definition.id, IAPData.REMOVE_ADS, System.StringComparison.Ordinal))
            {
                IAPAdapter.BuyRemoveAds();
                actionBuyRemoveAds?.Invoke();
            }
            else if(string.Equals(args.purchasedProduct.definition.id, IAPData.PACK_1, System.StringComparison.Ordinal)) 
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
    }
}

