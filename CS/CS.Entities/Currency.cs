//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CS.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Currency
    {
        public Currency()
        {
            this.Rate = new HashSet<Rate>();
            this.Reference = new HashSet<Reference>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    
        public virtual ICollection<Rate> Rate { get; set; }
        public virtual ICollection<Reference> Reference { get; set; }
    }
}