// Copyright (C) 2014-2015, phamtuan Research Inc.
//  
// All rights are reserved. Reproduction or transmission in whole or in part, in any form or by
// any means, electronic, mechanical or otherwise, is prohibited without the prior written
// consent of the copyright owner.
// ---------------------------------------------------------------------------------

#region

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using _3DExample.Annotations;

#endregion

namespace _3DExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region Properties

        public Single Robot1Axis1
        {
            get { return _Robot1Axis1; }
            set
            {
                _Robot1Axis1 = value;
                OnPropertyChanged();
            }
        }

        private Single _Robot1Axis1;

        /// <summary>
        /// Gets or sets the value of Robot1Axis2.
        /// </summary>
        public Single Robot1Axis2
        {
            get { return _Robot1Axis2; }
            set
            {
                _Robot1Axis2 = value;
                OnPropertyChanged();
            }
        }

        private Single _Robot1Axis2;

        /// <summary>
        /// Gets or sets the value of Robot1Axis3.
        /// </summary>
        public Single Robot1Axis3
        {
            get { return _Robot1Axis3; }
            set
            {
                _Robot1Axis3 = value;
                OnPropertyChanged();
            }
        }

        private Single _Robot1Axis3;

        /// <summary>
        /// Gets or sets the value of Robot1Axis4.
        /// </summary>
        public Single Robot1Axis4
        {
            get { return _Robot1Axis4; }
            set
            {
                _Robot1Axis4 = value;
                OnPropertyChanged();
            }
        }

        private Single _Robot1Axis4;

        public bool IsRuning
        {
            get { return _isRuning; }
            set
            {
                _isRuning = value;
                IsCanControl = !value;
                OnPropertyChanged();
            }
        }

        private bool _isRuning = true;


        public bool IsCanControl
        {
            get { return _isCanControl; }
            set
            {
                _isCanControl = value;
                OnPropertyChanged();
            }
        }

        private bool _isCanControl = false;

        #endregion


        private Timer _timer;
        private DateTime _timestamp;
        private double _masterAxis;
        private short _speed = 3;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            _timer = new Timer(OnScan, null, 300, 300);

        }

        private void OnScan(object state)
        {
            var dt = DateTime.UtcNow - _timestamp;
            _timestamp = DateTime.UtcNow;

            if (_isRuning)
            {
                double period;
                switch (_speed)
                {
                    case 1:
                        period = 20.0;
                        break;
                    case 2:
                        period = 10.0;
                        break;
                    case 3:
                        period = 5.0;
                        break;
                    default:
                        period = 30.0;
                        break;
                }

                _masterAxis = (_masterAxis + dt.TotalSeconds / period) % 1.0;

                // Set data
                Robot1Axis1 = (float)(Math.Sin(_masterAxis * 2.0 * Math.PI) * 45.0);
                Robot1Axis2 = (float)(Math.Cos(_masterAxis * 2.0 * Math.PI) * 45.0);
                Robot1Axis3 = (float)(Math.Sin(((_masterAxis * 2.0) % 1.0) * 2.0 * Math.PI) * 45.0);
                Robot1Axis4 = (float)(Math.Cos(_masterAxis * 2.0 * Math.PI) * -180.0);
            }
        }

        #region Implement for INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}