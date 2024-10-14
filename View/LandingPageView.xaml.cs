﻿using OsuReplayAnalyser.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OsuReplayAnalyser.View
{
    /// <summary>
    /// Interaction logic for LandingPageView.xaml
    /// </summary>
    public partial class LandingPageView : UserControl
    {
        public LandingPageView()
        {
            InitializeComponent();
            DataContext = new LandingPageViewModel();
        }
    }
}
