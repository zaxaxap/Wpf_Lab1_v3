using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DataLibrary
{

    public class ViewData: INotifyPropertyChanged
    {
        public VMBenchmark benchmarks;
        public bool Changed { get; set; }
        public ViewData()
        {
            Changed = false;
            benchmarks = new VMBenchmark();
        }
        public void BenchmarksClear()
        {
            benchmarks.accuracy.Clear();
            benchmarks.times.Clear();
            benchmarks.min_max_coef = new float[2] { 100, 0 };
            Changed = false;
            benchmarks.OnPropertyChanged("min_max_coef");
        }
        public void AddVMTime(VMGrid grid)
        {
            benchmarks.AddVMTime(grid);
            Changed = true;
            OnPropertyChanged("Changed");
        }
        public void AddVMAccuracy(VMGrid grid)
        {
            benchmarks.AddVMAccuracy(grid);
            Changed |= true;
            OnPropertyChanged("Changed");
        }
        public bool Save(string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Create);
                StreamWriter stream = new StreamWriter(fs);
                stream.WriteLine(benchmarks.min_max_coef[0]);
                stream.WriteLine(benchmarks.min_max_coef[1]);
                stream.WriteLine(benchmarks.accuracy.Count.ToString());
                stream.WriteLine(benchmarks.times.Count.ToString());
                foreach (var Accuracy in benchmarks.accuracy)
                {
                    stream.WriteLine(Accuracy.differ + " " + Accuracy.arg + " " + Accuracy.HA + " " 
                                   + Accuracy.EP + " " + Accuracy.net.nodes + " " + Accuracy.net.start 
                                   + " " + Accuracy.net.step + " " + Accuracy.net.end + " " + (int)Accuracy.net.func_type);
                }
                foreach (var Time in benchmarks.times)
                {
                    stream.WriteLine(Time.HA_time + " " + Time.EP_time + " " + Time.coef + " " 
                                   + Time.net.nodes + " " + Time.net.start + " " + Time.net.step + " " 
                                   + Time.net.end + " " + (int)Time.net.func_type);
                }
                stream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
            Changed = false;
            OnPropertyChanged("Changed");
            return true;
        }
        public bool Load(string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                StreamReader stream = new StreamReader(fs);
                benchmarks.min_max_coef[0] = float.Parse(stream.ReadLine());
                benchmarks.min_max_coef[1] = float.Parse(stream.ReadLine());
                int accuracyCount = int.Parse(stream.ReadLine());
                int timesCount = int.Parse(stream.ReadLine());
                benchmarks.OnPropertyChanged("min_max_coef");

                string[] measure = null;
                for (int i = 0; i < accuracyCount; i++)
                {
                   measure = stream.ReadLine().Split(' ');
                   benchmarks.accuracy.Add(new VMAccuracy(int.Parse(measure[4]), float.Parse(measure[5]),
                                                          float.Parse(measure[7]), (VMf)int.Parse(measure[8]),
                                                          float.Parse(measure[0]), float.Parse(measure[1]),
                                                          float.Parse(measure[2]), float.Parse(measure[3])));
                }
                for(int i = 0; i < timesCount; i++)
                {
                    measure = stream.ReadLine().Split(' ');
                    benchmarks.times.Add(new VMTime(int.Parse(measure[3]), float.Parse(measure[4]),
                                                    float.Parse(measure[6]), (VMf)int.Parse(measure[7]),
                                                    float.Parse(measure[0]), float.Parse(measure[1]),
                                                    float.Parse(measure[2])));
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
            Changed = false;
            OnPropertyChanged("Changed");
            return true;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }

}
