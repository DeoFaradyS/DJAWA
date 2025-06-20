```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.22631.5472/23H2/2023Update/SunValley3)
AMD Ryzen 5 5600H with Radeon Graphics 3.30GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 9.0.200
  [Host]     : .NET 8.0.13 (8.0.1325.6609), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.13 (8.0.1325.6609), X64 RyuJIT AVX2


```
| Method       | ExistingBarangCount | Mean     | Error     | StdDev   | Median   | Gen0      | Gen1     | Gen2     | Allocated |
|------------- |-------------------- |---------:|----------:|---------:|---------:|----------:|---------:|---------:|----------:|
| **AddNewBarang** | **5**                   | **9.698 ms** | **1.7973 ms** | **5.300 ms** | **9.360 ms** | **2078.1250** | **179.6875** | **164.0625** |  **16.57 MB** |
| **AddNewBarang** | **10**                  | **6.847 ms** | **1.1120 ms** | **3.279 ms** | **5.575 ms** | **1078.1250** | **140.6250** | **109.3750** |   **8.27 MB** |
| **AddNewBarang** | **15**                  | **7.105 ms** | **0.4112 ms** | **1.213 ms** | **7.102 ms** |  **500.0000** |  **93.7500** |  **31.2500** |   **4.14 MB** |
