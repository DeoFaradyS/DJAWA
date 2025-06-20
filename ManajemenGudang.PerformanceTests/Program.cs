using BenchmarkDotNet.Running;
using ManajemenGudang.PerformanceTests;
using System.Reflection; // Tambahkan using ini

public class Program
{
    public static void Main(string[] args)
    {
        // Baris ini akan secara otomatis menemukan dan menjalankan SEMUA class benchmark
        // yang ada di dalam proyek (Assembly) ini.
        // BenchmarkRunner.Run(Assembly.GetExecutingAssembly());
        BenchmarkRunner.Run<AdminDashboardViewModelBenchmarks>();
        // BenchmarkRunner.Run<RiwayatBarangViewModelBenchmarks>();
        // BenchmarkRunner.Run<TambahBarangViewModelBenchmarks>();
        // BenchmarkRunner.Run<LoginViewModelBenchmarks>();
        // BenchmarkRunner.Run<RegisterViewModelBenchmarks>();
    }
}