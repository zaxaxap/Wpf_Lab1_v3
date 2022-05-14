#include "pch.h"
#include <time.h>
#include "mkl.h"
#include <chrono>
using namespace std::chrono;

extern "C"  _declspec(dllexport)
void global_func(float start, float end,  float step, MKL_INT type, int& ret, float* differ, float* arg, float* HA, float* EP, float* HA_time, float* EP_time, float* coef)
{
	try
	{
		if (start == end) {
			ret = 0;
			return;
		}
		int n = (int)((end - start) / step);
		if (n < 0) {
			n = -n;
		}
		if (type == 0 || type == 2) {
			float* x = new float[n];
			float* HAm = new float[n];
			float* EPm = new float[n];
			float* sub = new float[n];
			float max = 0;
			float diff = 0;
			for (int i = 0; i < n; i++) {
				x[i] = start + step * i;
			}
			if (type == 0)
			{
				high_resolution_clock::time_point t1 = high_resolution_clock::now();
				vmsLn(n, x, HAm, VML_HA);
				high_resolution_clock::time_point t2 = high_resolution_clock::now();
				duration<float> time_span1 = t2 - t1;
				*HA_time = time_span1.count();

				high_resolution_clock::time_point t3 = high_resolution_clock::now();
				vmsLn(n, x, EPm, VML_EP);
				high_resolution_clock::time_point t4 = high_resolution_clock::now();
				duration<float> time_span2 = t4 - t3;
				*EP_time = time_span2.count();

				*coef = *HA_time / (*EP_time);

				for (int i = 0; i < n; i++) {
					if (abs(HAm[i] - EPm[i]) > max) {
						max = abs(HAm[i] - EPm[i]);
						*arg = x[i];
						*HA = HAm[i];
						*EP = EPm[i];
					}
				}
				*differ = max;
			}
			if (type == 2)
			{
				high_resolution_clock::time_point t1 = high_resolution_clock::now();
				vmsLGamma(n, x, HAm, VML_HA);
				high_resolution_clock::time_point t2 = high_resolution_clock::now();
				duration<float> time_span1 = t2 - t1;
				*HA_time = time_span1.count();

				high_resolution_clock::time_point t3 = high_resolution_clock::now();
				vmsLGamma(n, x, EPm, VML_EP);
				high_resolution_clock::time_point t4 = high_resolution_clock::now();
				duration<float> time_span2 = t4 - t3;
				*EP_time = time_span2.count();

				*coef = *HA_time / (*EP_time);

				for (int i = 0; i < n; i++) {
					if (abs(HAm[i] - EPm[i]) > max) {
						max = abs(HAm[i] - EPm[i]);
						*arg = x[i];
						*HA = HAm[i];
						*EP = EPm[i];
					}
				}
				*differ = max;
			}
		}
		if (type == 1 || type == 3) {
			double* x = new double[n];
			double* HAm = new double[n];
			double* EPm = new double[n];
			double* sub = new double[n];
			double max = 0;
			for (int i = 0; i < n; i++) {
				x[i] = start + step * i;
			}
			if (type == 1)
			{
				high_resolution_clock::time_point t1 = high_resolution_clock::now();
				vmdLn(n, x, HAm, VML_HA);
				high_resolution_clock::time_point t2 = high_resolution_clock::now();
				duration<float> time_span1 = t2 - t1;
				*HA_time = time_span1.count();

				high_resolution_clock::time_point t3 = high_resolution_clock::now();
				vmdLn(n, x, EPm, VML_EP);
				high_resolution_clock::time_point t4 = high_resolution_clock::now();
				duration<float> time_span2 = t4 - t3;
				*EP_time = time_span2.count();

				*coef = *HA_time / (*EP_time);

				for (int i = 0; i < n; i++) {
					if (abs(HAm[i] - EPm[i]) > max) {
						max = abs(HAm[i] - EPm[i]);
						*arg = x[i];
						*HA = HAm[i];
						*EP = EPm[i];
					}
				}
				*differ = max;
			}
			if (type == 3)
			{
				high_resolution_clock::time_point t1 = high_resolution_clock::now();
				vmdLGamma(n, x, HAm, VML_HA);
				high_resolution_clock::time_point t2 = high_resolution_clock::now();
				duration<float> time_span1 = t2 - t1;
				*HA_time = time_span1.count();

				high_resolution_clock::time_point t3 = high_resolution_clock::now();
				vmdLGamma(n, x, EPm, VML_EP);
				high_resolution_clock::time_point t4 = high_resolution_clock::now();
				duration<float> time_span2 = t4 - t3;
				*EP_time = time_span2.count();

				*coef = *HA_time / (*EP_time);

				for (int i = 0; i < n; i++) {
					if (abs(HAm[i] - EPm[i]) > max) {
						max = abs(HAm[i] - EPm[i]);
						*arg = x[i];
						*HA = HAm[i];
						*EP = EPm[i];
					}
				}
				*differ = max;
			}
		}
		ret = 0;
	}
	catch(...) {
		ret = -1;
	}

}