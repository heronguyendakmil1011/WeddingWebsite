//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WeddingStoreDesktop.Models.SystemModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class KhoVatLieu:BaseModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhoVatLieu()
        {
            this.ChiTietDonGiaNhapHangs = new HashSet<ChiTietDonGiaNhapHang>();
            this.ChiTietSanPhams = new HashSet<ChiTietSanPham>();
            this.PhatSinhs = new HashSet<PhatSinh>();
        }
    
        public string MaVL { get; set; }
        public string TenVL { get; set; }
        public Nullable<int> SoLuongTon { get; set; }
        public string DonVi { get; set; }
        public Nullable<float> GiaTien { get; set; }
        //private byte[] _AnhMoTa { get; set; }
        public byte[] AnhMoTa { get; set; }
        //public byte[] AnhMoTa { get => _AnhMoTa; set { _AnhMoTa = value; OnPropertyChanged(); } }
        public Nullable<bool> IsNhap { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonGiaNhapHang> ChiTietDonGiaNhapHangs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietSanPham> ChiTietSanPhams { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhatSinh> PhatSinhs { get; set; }
    }
}
