using System.Collections.Generic;
using System.Linq;

namespace ManajemenPesananSederhana
{
    public class StatusViewer : OrderViewer
    {
        public StatusViewer(List<Order> pesanan) : base(pesanan) { }

        protected override List<Order> Filter()
        {
            
            return daftarPesanan
                .Where(p => p.Status != "Selesai")
                .ToList();
        }
    }
}
