using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZSZ.AdminWeb.Models
{
    public class HouseEditPostModel
    {
        public long Id { get; set; }
        public long RegionId { get; set; }
        public long CommunityId { get; set; }
        public long RoomTypeId { get; set; }
        public string HouseAddress { get; set; }
        public int MonthRent { get; set; }
        public long StatusId { get; set; }
        public decimal HouseArea { get; set; }
        public long TypeId { get; set; }
        public long DecorateStatusId { get; set; }
        public int FloorIndex { get; set; }
        public int TotalIndex { get; set; }
        public string Direction { get; set; }
        public DateTime LookableDatetime { get; set; }
        public DateTime CheckInDate { get; set; }
        public string OwnerName { get; set; }
        public string OwnerPhoneNum { get; set; }
        public long[] AttachmentIds { get; set; }
        public string Description { get; set; }
    }
}