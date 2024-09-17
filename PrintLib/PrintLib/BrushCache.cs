using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PrintLib
{
    /// <summary>
    /// Cache of SolidBrush objects.
    /// </summary>
    /// Useful for conserving GDI resources, so that we're not allocating a new Brush every time we need one, and possibly squandering memory.
    /// Idea from http://stackoverflow.com/questions/9285536/caching-gdi-objects-in-a-winforms-application-is-it-worth-it-and-how-to-do-it
    public class BrushCache : IDisposable
    {
        [ThreadStatic]
        private static BrushCache _current = null; // the BrushCache singleton instance in the current thread
        private readonly Dictionary<Color, SolidBrush> _SolidBrushes = new Dictionary<Color, SolidBrush>();

        public BrushCache()
        {
            if (_current == null)
                _current = this;
        }

        public void Dispose()
        {
            if (_current == this)
                _current = null;

            foreach (var solidBrush in _SolidBrushes.Values)
                solidBrush.Dispose();
        }

        public static SolidBrush GetBrush(Color color)
        {
            if (_current == null)
                _current = new BrushCache();
            if (!_current._SolidBrushes.ContainsKey(color))
                _current._SolidBrushes[color] = new SolidBrush(color);

            return _current._SolidBrushes[color];
        }
    }
}
