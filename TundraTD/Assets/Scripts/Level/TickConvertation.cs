namespace Level
{
    static class TickConverter
    {
        public static int SecondsToTicks(this float seconds) => (int)(second * 10);
        public static float TickToSecond(int tick) => tick / 10;
    }
}
