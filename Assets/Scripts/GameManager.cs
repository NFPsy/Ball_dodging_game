public static class GameManager
{
    public static bool IsGameOver = false;
    public static bool IsGameClear = false;
    public const int GridSize = 5;
    public const float CellSize = 2f;
    public static float BestTime = 0f;

    public static void UpdateBestTime(float time)
    {
        if (time > BestTime)
        {
            BestTime = time;
        }
    }
}
