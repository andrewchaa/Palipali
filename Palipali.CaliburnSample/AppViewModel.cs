using System.ComponentModel.Composition;
using System.Windows.Media;
using Caliburn.Micro;

namespace Palipali.CaliburnSample
{
    [Export(typeof(AppViewModel))]
    public class AppViewModel : PropertyChangedBase, IHandle<ColourEvent>
    {
        [ImportingConstructor]
        public AppViewModel(ColourViewModel colourModel, IEventAggregator events)
        {
            ColourModel = colourModel;
            events.Subscribe(this);
        }

        private int _count = 50;
        public int Count
        {
            get { return _count;  }
            set 
            { 
                _count = value;
                NotifyOfPropertyChange(() => Count);
            }
        }

        public ColourViewModel ColourModel { get; private set; }

        public void IncrementCount(int delta)
        {
            Count += delta;
        }

        private SolidColorBrush _colour;
        public SolidColorBrush Colour
        {
            get { return _colour; }
            set 
            { 
                _colour = value;
                NotifyOfPropertyChange(() => Colour);
            }
        }


        public void Handle(ColourEvent message)
        {
            Colour = message.Colour;
        }
    }
}