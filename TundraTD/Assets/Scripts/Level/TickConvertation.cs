namespace Level
{
    static class TickConvertation
    {
        public static int SecondToTick(float second) => (int)(second * 10);
        public static float TickToSecond(int tick) => tick / 10;
    }
}
