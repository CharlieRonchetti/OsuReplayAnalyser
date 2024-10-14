using OsuReplayAnalyser.MVVM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OsuReplayAnalyser.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public RelayCommand UpdateViewCommand => new RelayCommand(execute => UpdateView(execute), canExecute => CanUpdateView(canExecute));

        private ViewModelBase? selectedViewModel = new LandingPageViewModel();

		public ViewModelBase? SelectedViewModel
        {
			get { return selectedViewModel; }
			set { 
                selectedViewModel = value;
                OnPropertyChanged();
            }
		}

        private Visibility landingButtonVisibility = Visibility.Collapsed;

        public Visibility LandingButtonVisibility
        {
            get { return landingButtonVisibility; }
            set {
                landingButtonVisibility = value; 
                OnPropertyChanged();
            }
        }


        private string? CurrentView = "Landing";

        public void UpdateView(object view)
        {
            Debug.WriteLine(view.ToString());
            if (view.ToString() == "HoldTimes")
            {
                this.SelectedViewModel = new HoldTimesViewModel();
                this.LandingButtonVisibility = Visibility.Visible; 
                this.CurrentView = view.ToString();
            }
            else if (view.ToString() == "Landing")
            {
                this.SelectedViewModel = new LandingPageViewModel();
                this.LandingButtonVisibility = Visibility.Collapsed;
                this.CurrentView = view.ToString();
            }
        }

        public bool CanUpdateView(object view)
        {
            if(view.ToString() != this.CurrentView)
            {
                return true;
            }

            return false;
        }
    }
}
