// --------------------------------------------------------------------------------
// tailor-made software GmbH
// --------------------------------------------------------------------------------
// project:      be-together
// filename:     ErrorModel.cs
// --------------------------------------------------------------------------------
// 
// Created:      2015-01-06   08:47
// 
// Last changed: 2015-01-06   09:02
//               Torsten Vogt (tvo)
// --------------------------------------------------------------------------------

namespace TMS.be_together.Models
{
    public class ErrorModel
    {
        public string RequestedUrl { get; set; }
        public string ReferrerUrl { get; set; }
    }
}