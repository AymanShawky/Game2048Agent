using System;
using System.Drawing;

namespace Game2048Core
{
    public class Tile : IEquatable<Tile>
    {
        public Tile(short value)
        {
            this.Value = value;
        }
        public short Value { get; set; }
        public bool IsMerged = false;

        public bool Equals(Tile other)
        {
            return this.Value == other.Value;
        }
    }
}
