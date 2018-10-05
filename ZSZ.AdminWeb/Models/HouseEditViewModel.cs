using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZSZ.DTO;

namespace ZSZ.AdminWeb.Models
{
    public class HouseEditViewModel
    {
        public HouseDTO House { get; set; }
        public RegionDTO[] Regions { get; set; }
        public IdNameDTO[] RoomTypes { get; set; }
        public IdNameDTO[] Statuses { get; set; }
        public IdNameDTO[] DecorateStatues { get; set; }
        public IdNameDTO[] Types { get; set; }
        public AttachmentDTO[] Attachments { get; set; }
    }
}