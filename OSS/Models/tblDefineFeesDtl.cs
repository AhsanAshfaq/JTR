//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OSS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblDefineFeesDtl
    {
        public int DefineFeesDtlID { get; set; }
        public Nullable<long> DefineFeesID { get; set; }
        public Nullable<int> FeesTypeID { get; set; }
    
        public virtual tblDefineFeesMst tblDefineFeesMst { get; set; }
        public virtual tblFeesType tblFeesType { get; set; }
    }
}