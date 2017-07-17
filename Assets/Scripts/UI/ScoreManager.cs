public class ScoreManager {

    public static int player1Score = 0;
    public static int player2Score = 0;

	public static void IncrementScore(int playernum)
    {
        if (playernum == 1)
        {
            player2Score++;
        } else
        {
            player1Score++;
        }

        if (player1Score >= GameData.stockCount)
        {
            GameLoader.EndGame(1);
        }
        else if (player2Score >= GameData.stockCount)
        {
            GameLoader.EndGame(2);
        }
    }
}
