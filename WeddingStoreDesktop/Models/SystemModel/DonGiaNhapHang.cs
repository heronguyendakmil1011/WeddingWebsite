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
    
    public partial class DonGiaNhapHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonGiaNhapHang()
        {
            this.ChiTietDonGiaNhapHangs = new HashSet<ChiTietDonGiaNhapHang>();
        }
    
        public string MaDG { get; set; }
        public Nullable<System.DateTime> NgayLap { get; set; }
        public Nullable<float> TongTien { get; set; }
        public string MaNCC { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonGiaNhapHang> ChiTietDonGiaNhapHangs { get; set; }
        public virtual NhaCungCap NhaCungCap { get; set; }
    }
}
