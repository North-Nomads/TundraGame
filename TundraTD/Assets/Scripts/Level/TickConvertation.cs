namespace Level
{
    static class TickConverter
    {
        public static int SecondsToTicks(this float seconds) => (int)(seconds * 10);
        public static float TicksToSeconds(this int tick) => tick / 10;
    }
}
