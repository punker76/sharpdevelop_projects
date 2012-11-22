/*
 * Created by SharpDevelop.
 * User: punker76
 * Date: 22.11.2012
 * Time: 20:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using OxyPlot;

namespace chart_control
{
  /// <summary>
  /// Interaction logic for Window1.xaml
  /// </summary>
  public partial class Window1 : Window
  {
    public class MyData : INotifyPropertyChanged, IDataPoint, ICodeGenerating
    {
      public event PropertyChangedEventHandler PropertyChanged;

      // This method is called by the Set accessor of each property.
      // The CallerMemberName attribute that is applied to the optional propertyName
      // parameter causes the property name of the caller to be substituted as an argument.
      private void NotifyPropertyChanged(String propertyName = "")
      {
        if (PropertyChanged != null)
        {
          PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
      }
      
      private double x;
      private double y;
      
      public double X
      {
        get
        {
          return this.x;
        }
        set
        {
          if (value != this.x)
          {
            this.x = value;
            NotifyPropertyChanged("X");
          }
        }
      }

      public double Y
      {
        get
        {
          return this.y;
        }
        set
        {
          if (value != this.y)
          {
            this.y = value;
            NotifyPropertyChanged("Y");
          }
        }
      }
      
      public string ToCode()
      {
        return "huhu";
      }
    }
    
    public static readonly DependencyProperty MyCollectionProperty =
      DependencyProperty.Register("MyCollection", typeof(ObservableCollection<MyData>), typeof(Window1),
                                  new FrameworkPropertyMetadata(new ObservableCollection<MyData>()));
    
    public ObservableCollection<MyData> MyCollection {
      get { return (ObservableCollection<MyData>)GetValue(MyCollectionProperty); }
      set { SetValue(MyCollectionProperty, value); }
    }
    
    private BackgroundWorker bw;
    
    public Window1()
    {
      InitializeComponent();
      
      this.bw = new BackgroundWorker();
      bw.DoWork += delegate(object sender, DoWorkEventArgs e)
      {
        var rand = new Random(555);
        for (int i = 0; i < 100000; i++) {
          Thread.Sleep(100);
          var data = new MyData() { X = i+10, Y = rand.Next(-10,35) };
          ((BackgroundWorker)sender).ReportProgress(i, data);
        }
      };
      bw.WorkerReportsProgress = true;
      bw.ProgressChanged += delegate(object sender, ProgressChangedEventArgs e)
      {
        this.MyCollection.Add((MyData)e.UserState);
        if(e.ProgressPercentage % 10 == 0)
        {
          plot.RefreshPlot(true);
        }
      };
      bw.RunWorkerAsync();
    }
  }
}