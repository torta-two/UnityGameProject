using UnityEngine.UI;
using UnityEngine;

public class SystemSettingPanel : BasePanel
{
    private Slider bgmVolume;
    private Slider effectVolume;

    protected override void Awake()
    {
        base.Awake();

        bgmVolume = transform.Find("BgmVolume/Slider").GetComponent<Slider>();
        effectVolume = transform.Find("EffectVolume/Slider").GetComponent<Slider>();

        ctrl.audioManager.bgmVolume = bgmVolume.value * 0.6f;
        ctrl.audioManager.effectVolume = effectVolume.value;
    }

    private void Update()
    {
        ctrl.audioManager.bgmVolume = bgmVolume.value * 0.6f;
        ctrl.audioManager.effectVolume = effectVolume.value;
    }

}
