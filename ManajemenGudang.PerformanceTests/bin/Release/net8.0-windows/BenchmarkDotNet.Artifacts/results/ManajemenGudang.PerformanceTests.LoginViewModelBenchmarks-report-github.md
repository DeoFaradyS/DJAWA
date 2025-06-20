```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.22631.5472/23H2/2023Update/SunValley3)
AMD Ryzen 5 5600H with Radeon Graphics 3.30GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 9.0.200
  [Host]     : .NET 8.0.13 (8.0.1325.6609), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.13 (8.0.1325.6609), X64 RyuJIT AVX2


```
| Method                    | UserCount | Mean         | Error        | StdDev       | Gen0   | Gen1   | Allocated |
|-------------------------- |---------- |-------------:|-------------:|-------------:|-------:|-------:|----------:|
| **&#39;Admin Login&#39;**             | **5**         |     **11.56 ns** |     **0.256 ns** |     **0.262 ns** | **0.0038** |      **-** |      **32 B** |
| &#39;Valid User Login&#39;        | 5         | 58,827.78 ns | 1,089.511 ns |   850.618 ns | 1.2207 | 0.2441 |   12168 B |
| &#39;Non-Existent User Login&#39; | 5         | 55,895.22 ns | 1,078.629 ns | 1,402.523 ns | 1.2207 | 0.2441 |   11808 B |
| **&#39;Admin Login&#39;**             | **10**        |     **11.45 ns** |     **0.234 ns** |     **0.329 ns** | **0.0038** |      **-** |      **32 B** |
| &#39;Valid User Login&#39;        | 10        | 58,203.47 ns | 1,063.504 ns |   888.074 ns | 1.2207 | 0.2441 |   12056 B |
| &#39;Non-Existent User Login&#39; | 10        | 56,892.17 ns | 1,050.803 ns |   820.398 ns | 1.2207 | 0.2441 |   11808 B |
| **&#39;Admin Login&#39;**             | **15**        |     **11.29 ns** |     **0.102 ns** |     **0.085 ns** | **0.0038** |      **-** |      **32 B** |
| &#39;Valid User Login&#39;        | 15        | 56,285.08 ns |   887.025 ns |   692.531 ns | 1.2207 | 0.2441 |   12056 B |
| &#39;Non-Existent User Login&#39; | 15        | 57,701.85 ns | 1,082.985 ns | 1,289.217 ns | 1.2207 | 0.2441 |   11808 B |
