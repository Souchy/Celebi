namespace souchy.celebi.eevee.enums
{
    /// <summary>
    /// TODO: Wont need this anymore I believe with ContextualProperty stats
    /// </summary>
    public enum ContextType
    {
        Fight, // current fight
        Round, // current round
        Turn, // current turn
        Action, // aka a spell or a move
        Effect // current effect (a context for each effect and their children might be too much)
    }
}
