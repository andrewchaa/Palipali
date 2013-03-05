using System.ComponentModel.Composition;
using System.Windows.Media;
using Caliburn.Micro;

namespace Palipali.CaliburnSample
{
    [Export (typeof(ColourViewModel))]
    public class ColourViewModel
    {
        private readonly IEventAggregator _events;

        [ImportingConstructor]
        public ColourViewModel(IEventAggregator events)
        {
            _events = events;
        }

        public void Red()
        {
            _events.Publish(new ColourEvent(new SolidColorBrush(Colors.Red)));
        }

        public void Green()
        {
            _events.Publish(new ColourEvent(new SolidColorBrush(Colors.Green)));
        }
        
        public void Blue()
        {
            _events.Publish(new ColourEvent(new SolidColorBrush(Colors.Blue)));
        }
    }
}