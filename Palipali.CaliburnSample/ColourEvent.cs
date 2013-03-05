using System.Windows.Media;

namespace Palipali.CaliburnSample
{
    public class ColourEvent
    {
        public SolidColorBrush Colour { get; private set; }

        public ColourEvent(SolidColorBrush colour)
        {
            Colour = colour;
        }
    }
}