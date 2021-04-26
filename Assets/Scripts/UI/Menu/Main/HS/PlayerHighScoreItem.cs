using System;

[Serializable]
public class PlayerHighScoreItem
{
    public string PlayerName { get; set; }

    public string ScoreResult { get; set; }

    public DateTime Record { get; set; }
}