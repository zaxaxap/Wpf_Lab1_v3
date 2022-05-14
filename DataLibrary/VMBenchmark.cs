using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DataLibrary
{
    public class VMBenchmark : INotifyPropertyChanged
    {

        public VMBenchmark()
        {
            min_max_coef = new float[2] {100, 0};

            times = new ObservableCollection<VMTime> { };
            accuracy = new ObservableCollection<VMAccuracy> { };
        }

        public void calculate_min_max()
        {
            for (int i = 0; i < times.Count; i++)
            {
                if (times[i].coef < min_max_coef[0])
                {
                    min_max_coef[0] = times[i].coef;
                }
            }
            for (int i = 0; i < times.Count; i++)
            {
                if(times[i].coef > min_max_coef[1])
                {
                    min_max_coef[1] = times[i].coef;
                }
            }
        }
        public ObservableCollection<VMTime>? times;
        public ObservableCollection<VMAccuracy>? accuracy;
        public float[] min_max_coef { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public void AddVMTime(VMGrid param)
        {
            int ret = 0;
            float differ = 0;
            float arg = 0;
            float HA = 0;
            float EP = 0;
            float HA_time = 0;
            float EP_time = 0;
            float coef = 0;
            try
            {
                global_func(param.start, param.end, param.step, (int)param.func_type, ref ret, ref differ, ref arg, ref HA, ref EP, ref HA_time, ref EP_time, ref coef);
            }
            catch {
                throw;
            }
             times.Add(new VMTime(param.nodes, param.start, param.end, param.func_type, HA_time, EP_time, coef));
            calculate_min_max();
            OnPropertyChanged("min_max_coef");
        }
        public void AddVMAccuracy(VMGrid param)
        {
            int ret = 0;
            float differ = 0;
            float arg = 0;
            float HA = 0;
            float EP = 0;
            float HA_time = 0;
            float EP_time = 0;
            float coef = 0;
            try
            {
                global_func(param.start, param.end, param.step, (int)param.func_type, ref ret, ref differ, ref arg, ref HA, ref EP, ref HA_time, ref EP_time, ref coef);
            }
            catch
            {
                throw;
            }
            accuracy.Add(new VMAccuracy(param.nodes, param.start, param.end, param.func_type, differ, arg, HA, EP));
        }
        [DllImport("..\\..\\..\\..\\x64\\Debug\\CPP_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern
        void global_func(float start, float end, float step, int type, ref int ret, ref float differ, ref float arg, ref float HA, ref float EP, ref float HA_time, ref float EP_time, ref float coef);
        
    }
}
