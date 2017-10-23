//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace bus_timetables
{
    using System;
    using System.Collections.Generic;
    
    public partial class Route
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Route()
        {
            this.FavRoutes = new HashSet<FavRoute>();
            this.RouteStops = new HashSet<RouteStop>();
        }
    
        public int RouteID { get; set; }
        public string RouteNote { get; set; }
        public int DirectionID { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public Nullable<System.DateTime> RouteStart { get; set; }
        public Nullable<System.DateTime> RouteEnd { get; set; }
        public Nullable<double> RouteDuration { get; set; }
    
        public virtual Direction Direction { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FavRoute> FavRoutes { get; set; }
        public virtual Staff Staff { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RouteStop> RouteStops { get; set; }
    }
}
