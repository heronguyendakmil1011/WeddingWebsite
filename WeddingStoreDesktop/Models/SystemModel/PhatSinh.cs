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
    
    public partial class PhatSinh
    {
        public string MaHD { get; set; }
        public string MaVL { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<float> ThanhTien { get; set; }
    
        public virtual HoaDon HoaDon { get; set; }
        public virtual KhoVatLieu KhoVatLieu { get; set; }
    }
}
