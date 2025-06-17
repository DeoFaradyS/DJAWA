using System.Collections.Generic;
using System.Linq;

namespace ManajemenPesananSederhana
{
    public class RiwayatViewer : OrderViewer
    {
        public RiwayatViewer(List<Order> pesanan) : base(pesanan) { }

        protected override List<Order> Filter()
        {
            return daftarPesanan
                .Where(p => p.Status == "Selesai")
                .ToList();
        }
    }
}
