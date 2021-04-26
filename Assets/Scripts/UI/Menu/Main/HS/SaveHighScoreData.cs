using System;

[Serializable]
public class SaveHighScoreData
{
    public DateTime LastUpdate;
    public PlayerHighScoreItem[] Results;

    public SaveHighScoreData()
    {
        var dt = DateTime.Now;
        this.LastUpdate = dt;
        
        this.Results = new[]
        {
            new PlayerHighScoreItem { PlayerName = "---", ScoreResult = "---", Record = dt.AddMilliseconds(-1) },
            new PlayerHighScoreItem{ PlayerName = "---", ScoreResult = "---", Record = dt.AddMilliseconds(-2) },
            new PlayerHighScoreItem{ PlayerName = "---", ScoreResult = "---", Record = dt.AddMilliseconds(-3) },
            new PlayerHighScoreItem{ PlayerName = "---", ScoreResult = "---", Record = dt.AddMilliseconds(-4) },
            new PlayerHighScoreItem{ PlayerName = "---", ScoreResult = "---", Record = dt.AddMilliseconds(-5) }
        };
    }
    
    
}