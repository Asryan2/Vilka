//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vilka.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class RegionsDictionaryOptimizationData
    {
        public int ID { get; set; }
        public int RegionID { get; set; }
        public Nullable<int> WikiPageID { get; set; }
    
        public virtual Region Region { get; set; }
    }
}