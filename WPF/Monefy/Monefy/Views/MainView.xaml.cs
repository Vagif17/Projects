﻿using LiveCharts;
using LiveCharts.Wpf;
using Monefy.Models;
using Monefy.ViewModels;
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

namespace Monefy;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
/// 

public partial class MainWindow : Window

{

    public MainWindow()
    {
        InitializeComponent();
    }


    private  void minimizeBtn_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
   
    }

    private void maximizeBtn_Click(object sender, RoutedEventArgs e)
    { 
        if(WindowState == WindowState.Normal) { WindowState = WindowState.Maximized; }
        else { WindowState = WindowState.Normal; }
    }

    private void closeBtn_Click(object sender, RoutedEventArgs e)
    {
        App.Current.Shutdown();
    }

    private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
    {
        DragMove();
    }

    
    

   
}
