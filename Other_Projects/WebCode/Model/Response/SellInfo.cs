using System;

namespace HuaweiSoftware.HOP.Model
{
    public class SellInfo
    {
        public long ID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public short Type { get; set; }

        public int Amount { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public decimal TotalPrice { get; set; }

        public string Barcode { get; set; }

        public DateTime Time { get; set; }

        public string Creator { get; set; }
    }
}
