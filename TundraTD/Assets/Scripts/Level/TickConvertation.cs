namespace Level
{
    static class TickConverter
    {
        public static int SecondToTick(float second) => (int)(second * 10);
        public static float TickToSecond(int tick) => tick / 10;
    }
}
