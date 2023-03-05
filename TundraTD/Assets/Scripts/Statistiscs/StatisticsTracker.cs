using UnityEngine.Analytics;

public static class StatisticsTracker
{
    public static float PlayTime;
    //0 - Метеор; 1 - Водичка; 2 - Земелька
    public static int[] SpellsUsed = new int[3];
    //0-5 - Улучшения башни огня; 6-11 - Улучшения башни воды; 12-17 - Улучшение башни земли;
    public static int[] UpgradesBought = new int[18]; 
}
