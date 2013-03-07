using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace Palipali
{
    public class SearchResult
    {
        private readonly string _name;
        private readonly string _command;
        private readonly string _shortcut;
        private System.Windows.Media.ImageSource _icon;

        public string Name { get { return _name; } }
        public string Command { get { return _command; } }
        public string Shortcut { get { return _shortcut; } }

        public SearchResult() : this("Calculator", System.Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\calc.exe", string.Empty) {}
        public SearchResult(string name, string command, string shortcut)
        {
            _name = name;
            _command = command;
            _shortcut = shortcut;
        }

        /// <summary>Gets a WPF TmageSource for the icon to display for the search result</summary>
        public System.Windows.Media.ImageSource Icon
        {
            get
            {
                if (_icon == null && System.IO.File.Exists(Command))
                {
                    using (System.Drawing.Icon sysicon = System.Drawing.Icon.ExtractAssociatedIcon(Command))
                    {
                        // This new call in WPF finally allows us to read/display 32bit Windows file icons!
                        _icon = Imaging.CreateBitmapSourceFromHIcon(
                          sysicon.Handle,
                          System.Windows.Int32Rect.Empty,
                          System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
                    }
                }

                return _icon;
            }
        }
    }
}
