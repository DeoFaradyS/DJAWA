```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.22631.5472/23H2/2023Update/SunValley3)
AMD Ryzen 5 5600H with Radeon Graphics 3.30GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 9.0.200
  [Host]     : .NET 8.0.13 (8.0.1325.6609), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.13 (8.0.1325.6609), X64 RyuJIT AVX2


```
| Method          | ExistingUserCount | Mean      | Error     | StdDev   | Gen0      | Gen1    | Gen2    | Allocated |
|---------------- |------------------ |----------:|----------:|---------:|----------:|--------:|--------:|----------:|
| **RegisterNewUser** | **5**                 | **14.847 ms** | **2.8540 ms** | **8.415 ms** | **2488.2813** | **44.9219** | **21.4844** |  **20.09 MB** |
| **RegisterNewUser** | **10**                |  **5.686 ms** | **0.6193 ms** | **1.826 ms** |  **632.8125** | **39.0625** | **15.6250** |   **5.08 MB** |
| **RegisterNewUser** | **15**                | **16.623 ms** | **3.2382 ms** | **9.548 ms** | **2488.2813** | **44.9219** | **21.4844** |  **20.09 MB** |
