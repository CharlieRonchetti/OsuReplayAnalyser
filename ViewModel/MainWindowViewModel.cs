using OsuReplayAnalyser.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuReplayAnalyser.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
		private ViewModelBase? selectedViewModel;

		public ViewModelBase? SelectedViewModel
        {
			get { return selectedViewModel; }
			set { selectedViewModel = value; }
		}

	}
}
