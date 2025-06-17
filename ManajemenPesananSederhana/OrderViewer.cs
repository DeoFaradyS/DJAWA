using System.Collections.Generic;

namespace ManajemenPesananSederhana
{
    public abstract class OrderViewer
    {
        protected List<Order> daftarPesanan;

        public OrderViewer(List<Order> pesanan)
        {
            daftarPesanan = pesanan;
        }

        public List<Order> Tampilkan()
        {
            return Filter();
        }

        protected abstract List<Order> Filter();
    }
}
