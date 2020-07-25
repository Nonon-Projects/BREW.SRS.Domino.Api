using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace BREW.SRS.Domino.Application.Shared.Locator.Dto
{
    public class LocatorDto : EntityDto<long>
    {
        public string Ip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string HostName { get; set; }
        public string Location { get; set; }
        public string Org { get; set; }
        public string Postal { get; set; }
        public string Region { get; set; }
        public DateTime CreationTime{ get; set; }
    }
}
