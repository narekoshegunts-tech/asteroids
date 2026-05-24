using System;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;

namespace Game.Scripts.SDK
{
    public class YandexBannerService
    {
        private Banner _banner;
        private bool _isShown = false;
    
        private readonly string _adUnitId;

        public YandexBannerService(string adUnitId = "demo-banner-yandex")
        {
            _adUnitId = adUnitId;
        }

        public void Show()
        {
            if (_banner != null)
            {
                if (!_isShown)
                {
                    _banner.Show();
                    _isShown = true;
                }
                return;
            }

            RequestBanner();
        }

        public void Hide()
        {
            if (_banner != null)
            {
                _banner.Hide();
                _isShown = false;
            }
        }

        public void Destroy()
        {
            if (_banner != null)
            {
                _banner.Destroy();
                _banner = null;
                _isShown = false;
            }
        }

        private void RequestBanner()
        {
            if (_banner != null)
            {
                _banner.Destroy();
            }

            YandexAds.SetAgeRestricted(true);

            BannerAdSize bannerSize = BannerAdSize.Sticky(GetScreenWidthDp());

            _banner = new Banner(bannerSize, AdPosition.BottomCenter);

            // Подписка на события
            _banner.OnAdLoaded += HandleAdLoaded;
            _banner.OnAdFailedToLoad += HandleAdFailedToLoad;
            _banner.OnAdClicked += HandleAdClicked;
            _banner.OnImpression += HandleImpression;

            _banner.LoadAd(CreateAdRequest());

            Debug.Log("[YandexBannerService] Banner requested");
        }

        private AdRequest CreateAdRequest()
        {
            return new AdRequest(_adUnitId);
        }

        private int GetScreenWidthDp()
        {
            int screenWidth = (int)Screen.safeArea.width;
            return ScreenUtils.ConvertPixelsToDp(screenWidth);
        }

        // ====================== EVENTS ======================

        private void HandleAdLoaded(object sender, EventArgs args)
        {
            Debug.Log("[YandexBannerService] Banner loaded");
            if (_banner != null)
            {
                _banner.Show();
                _isShown = true;
            }
        }

        private void HandleAdFailedToLoad(object sender, AdFailureEventArgs args)
        {
            Debug.LogError($"[YandexBannerService] Banner failed to load: {args.Message}");
        }

        private void HandleAdClicked(object sender, EventArgs args)
        {
            Debug.Log("[YandexBannerService] Banner clicked");
        }

        private void HandleImpression(object sender, ImpressionData impressionData)
        {
            string data = impressionData?.rawData ?? "null";
            Debug.Log($"[YandexBannerService] Impression recorded: {data}");
        }
    }
}