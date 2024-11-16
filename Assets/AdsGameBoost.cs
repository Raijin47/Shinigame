using UnityEngine;
using YG;

public class AdsGameBoost: MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private GameObject _thxForWatch;

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += RewardedAds;
    }
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= RewardedAds;
    }

    public void Ads(int id)
    {
        YandexGame.RewVideoShow(id);
    }

    private void RewardedAds(int x)
    {
        switch(x)
        {
            case 1: 
                _character.AdsBoost();
                _thxForWatch.SetActive(true);
                break;
            case 2:
                EssentialService.instance.finalStatictic.RewardADs();
                break;
        }
    }
}