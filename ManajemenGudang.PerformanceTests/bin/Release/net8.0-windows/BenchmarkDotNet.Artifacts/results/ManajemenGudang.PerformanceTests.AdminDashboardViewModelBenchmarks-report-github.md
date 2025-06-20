```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.22631.5472/23H2/2023Update/SunValley3)
AMD Ryzen 5 5600H with Radeon Graphics 3.30GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 9.0.200
  [Host] : .NET 8.0.13 (8.0.1325.6609), X64 RyuJIT AVX2

Job=.NET 8.0  Runtime=.NET 8.0  

```
| Method                         | UserCount | Mean | Error |
|------------------------------- |---------- |-----:|------:|
| &#39;Load All Users (Constructor)&#39; | 10        |   NA |    NA |

Benchmarks with issues:
  AdminDashboardViewModelBenchmarks.'Load All Users (Constructor)': .NET 8.0(Runtime=.NET 8.0) [UserCount=10]
