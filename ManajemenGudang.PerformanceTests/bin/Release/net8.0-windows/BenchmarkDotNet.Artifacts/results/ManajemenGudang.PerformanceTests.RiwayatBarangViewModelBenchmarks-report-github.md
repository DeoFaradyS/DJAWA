```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.22631.5472/23H2/2023Update/SunValley3)
AMD Ryzen 5 5600H with Radeon Graphics 3.30GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 9.0.200
  [Host]     : .NET 8.0.13 (8.0.1325.6609), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.13 (8.0.1325.6609), X64 RyuJIT AVX2


```
| Method                 | BarangPerUserCount | Mean      | Error    | StdDev   | Median    | Gen0   | Gen1   | Allocated |
|----------------------- |------------------- |----------:|---------:|---------:|----------:|-------:|-------:|----------:|
| **&#39;Initial History Load&#39;** | **10**                 |  **64.60 μs** | **1.282 μs** | **2.894 μs** |  **63.30 μs** | **1.7090** | **0.4883** |  **15.01 KB** |
| &#39;Search For Item&#39;      | 10                 | 152.08 μs | 2.887 μs | 3.436 μs | 152.49 μs | 3.4180 | 0.9766 |  29.62 KB |
| **&#39;Initial History Load&#39;** | **30**                 |  **71.63 μs** | **1.146 μs** | **0.895 μs** |  **71.54 μs** | **2.4414** | **0.4883** |   **21.8 KB** |
| &#39;Search For Item&#39;      | 30                 | 164.31 μs | 3.120 μs | 3.064 μs | 164.93 μs | 4.3945 | 1.4648 |  36.42 KB |
