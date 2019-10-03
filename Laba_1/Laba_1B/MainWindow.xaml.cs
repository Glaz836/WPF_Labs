﻿using System;
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
using System.Windows.Threading;

namespace Laba_1B
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Elevator elevator;

        private DispatcherTimer dispatcherTimer;

        public MainWindow()
        {
            InitializeComponent();            
        }

        private void Go_Click(object sender, RoutedEventArgs e)
        {
            if (Floor.Text == "")
            {
                Go.Content = "ПОЕХАЛИ";
                Floors.Text = "5";
                elevator = new Elevator(5);
                FloorNow.Text = "1";
            }
            else
            {
                FloorNow.Text = "1";
                Go.Content = "ПОЕХАЛИ";
                elevator = new Elevator(int.Parse(Floor.Text));
                Floors.Text = Floor.Text;
            }
            Go.Click -= Go_Click;
            Go.Click += Poehaly;
        }

        private void Poehaly(object sender, RoutedEventArgs e)
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            Go.IsEnabled = false;
            State.Text = elevator.OpenTheDoor();
            elevator.Way = int.Parse(Floor.Text);
            if (int.Parse(Floor.Text) > 0 || int.Parse(Floor.Text) <= elevator.MaxFloor)
                dispatcherTimer.Start();
            else
                MessageBox.Show("ТАКОГО ЭТАЖА НЕТ!!!");
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            switch(elevator.Stage)
            {
                case 0:
                    State.Text = elevator.CloseTheDoor();
                    elevator.Stage++;
                    break;
                case 1:
                    if (elevator.Way > elevator.Now)
                    {
                        State.Text = elevator.Upstairs();
                        FloorNow.Text = elevator.Now.ToString();
                    }
                    else
                    {
                        State.Text = elevator.Downstairs();
                        FloorNow.Text = elevator.Now.ToString();
                    }
                    if (elevator.Now == elevator.Way) elevator.Stage++;
                    break;
                case 2:
                    State.Text = elevator.OpenTheDoor();
                    elevator.Stage++;
                    break;
                case 3:
                    State.Text = elevator.CloseTheDoor();
                    elevator.Stage = 0;
                    Go.IsEnabled = true;
                    dispatcherTimer.Stop();
                    break;
            }
        }
    }
}
