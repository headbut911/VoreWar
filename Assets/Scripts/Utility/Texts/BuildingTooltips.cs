static class BuildingTooltips
{
    internal static string Tooltip(int tooltip)
    {
        switch (tooltip)
        {
            case 1:
                return "Enables building button and allows the AI to build enabled buildings. Does not disable buildings placed in the map editor.";
            case 2:
                return "Prevents buildings from being built until this turn.";
            case 3:
                return "Affects the range of some buildings effects. Recommended: 3-5;";
            default:
                return "";
        }
    }
}
