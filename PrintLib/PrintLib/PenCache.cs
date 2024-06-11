using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PrintLib
{
    /// <summary>
    /// Cache of Pen objects.
    /// </summary>
    /// Useful for conserving GDI resources, so that we're not allocating a new Pen every time we need one, and possibly squandering memory.
    /// Idea from http://stackoverflow.com/questions/9285536/caching-gdi-objects-in-a-winforms-application-is-it-worth-it-and-how-to-do-it
    public class PenCache : IDisposable
    {
        [ThreadStatic]
        private static PenCache _current = null; // the PenCache singleton instance in the current thread
        private readonly Dictionary<ColorAndWidth, Pen> _Pens = new Dictionary<ColorAndWidth, Pen>();

        public PenCache()
        {
            if (_current == null)
                _current = this;
        }

        public void Dispose()
        {
            if (_current == this)
                _current = null;

            foreach (var pen in _Pens.Values)
                pen.Dispose();
        }

        public static Pen GetPen(Color color, decimal width)
        {
            if (_current == null)
                _current = new PenCache();
            if (!_current._Pens.ContainsKey(new ColorAndWidth(color, width)))
                _current._Pens[new ColorAndWidth(color, width)] = new Pen(color, (float)width);

            return _current._Pens[new ColorAndWidth(color, width)];
        }

        struct ColorAndWidth
        {
            public Color _Color;
            public Decimal _Width;
            public ColorAndWidth(Color color, Decimal width)
            {
                _Color = color;
                _Width = width;
            }

            public override int GetHashCode()
            {
                return _Color.GetHashCode();
            }
        }
    }
}
