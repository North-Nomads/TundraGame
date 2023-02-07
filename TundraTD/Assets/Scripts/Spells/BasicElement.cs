using System;

namespace Spells
{
    /// <summary>
    /// A struct that has every element used in game
    /// </summary>
    [Flags]
    public enum BasicElement
    {
        None = 0,
        Fire = 1,
        Water = 1 << 1,
        Earth = 1 << 2,
        Lightning = 1 << 3,
        Air = 1 << 4
    }
}