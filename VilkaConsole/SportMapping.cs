//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VilkaConsole
{
    using System;
    using System.Collections.Generic;
    
    public partial class SportMapping
    {
        public int ID { get; set; }
        public int SiteID { get; set; }
        public int SportID { get; set; }
        public string Name { get; set; }
    
        public virtual Site Site { get; set; }
        public virtual Sport Sport { get; set; }
    }
}
