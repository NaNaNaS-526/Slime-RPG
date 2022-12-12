using System;

public static class EventBus
{
    public static Action OnDead;
    public static Action<float> OnMoneyUpdated;
    public static Action OnAttackDamageEnhanced;
    public static Action OnAttackSpeedEnhanced;
    public static Action<int> OnSpentMoney;
    public static Action<int> OnGotMoney;

    public static Action<int> OnAttackDamageUpdated;
    public static Action<float> OnAttackSpeedUpdated;
}