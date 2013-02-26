using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Palipali
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Cache _cache;
        public Cache Cache { get { return _cache; } }

        protected override void OnStartup(StartupEventArgs e)
        {
            _cache = new Cache();
            base.OnStartup(e);
        }
    }
}
