using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Filters.Watermarks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.AdminWeb.App_Start;
using ZSZ.AdminWeb.Models;
using ZSZ.Common;
using ZSZ.CommonMVC;
using ZSZ.DTO;
using ZSZ.IService;


namespace ZSZ.AdminWeb.Controllers
{
    public class HouseController : Controller
    {
        public IHouseService HouseService { get; set; }
        public IAdminUserService AdminUserService { get; set; }
        public IRegionService RegionService { get; set; }
        public IIdNameService IdNameService { get; set; }
        public IAttachmentService AttachmentService { get; set; }
        public ICommunityService CommunityService { get; set; }
        // GET: House
        [CheckPermission("House.List")]
        public ActionResult List(long typeId)
        {
            long adminUserId = (long) Session["LoginUserId"];
            long? cityId = AdminUserService.GetById(adminUserId).CityId;
            if (cityId == null)
            {
                return View("Error", (object) "总部人员不能管理房源");
            }
            var models =  HouseService.GetPagedData(cityId.Value, typeId, 10, 0);
            return View(models);
        }
        [CheckPermission("House.Add")]
        public ActionResult Add()
        {
            long adminUserId = (long)Session["LoginUserId"];
            long? cityId = AdminUserService.GetById(adminUserId).CityId;
            if (cityId == null)
            {
                return View("Error", (object)"总部人员不能管理房源");
            }
            var regions = RegionService.GetAll(cityId.Value);
            var roomTypes = IdNameService.GetAll("户型");
            var statuses = IdNameService.GetAll("房屋状态");
            var decorationStatuses = IdNameService.GetAll("装修状态");
            var types = IdNameService.GetAll("房屋类别");
            var attachments = AttachmentService.GetAll();
            HouseAddViewModel model = new HouseAddViewModel()
            {
                Regions = regions,
                RoomTypes = roomTypes,
                Statuses = statuses,
                DecorateStatues = decorationStatuses,
                Types = types,
                Attachments = attachments
            };
            return View(model);

        }
        [HttpPost]
        [ValidateInput(false)]
        [CheckPermission("House.Add")]
        public ActionResult Add(HouseAddPostModel model)
        {
            long adminUserId = (long)Session["LoginUserId"];
            long? cityId = AdminUserService.GetById(adminUserId).CityId;
            if (cityId == null)
            {
                return View("Error", (object)"总部人员不能管理房源");
            }
            var houseDTO = new HouseAddNewDTO();
            houseDTO.Address = model.HouseAddress;
            houseDTO.AttachmentIds = model.AttachmentIds;
            houseDTO.CheckInDateTime = model.CheckInDate;
            houseDTO.CommunityId = model.CommunityId;
            houseDTO.DecorateStatusId = model.DecorateStatusId;
            houseDTO.Description = model.Description;
            houseDTO.Direction = model.Direction;
            houseDTO.FloorIndex = model.FloorIndex;
            houseDTO.TotalFloorCount = model.TotalIndex;
            houseDTO.LookableDateTime = model.LookableDatetime;
            houseDTO.MonthRent = model.MonthRent;
            houseDTO.OwnerName = model.OwnerName;
            houseDTO.OwnerPhoneNum = model.OwnerPhoneNum;
            houseDTO.RoomTypeId = model.RoomTypeId;
            houseDTO.StatusId = model.StatusId;
            houseDTO.TotalFloorCount = model.TotalIndex;
            houseDTO.TypeId = model.TypeId;
            houseDTO.Area = model.HouseArea;
            HouseService.AddNew(houseDTO);
            return Json(new AjaxResult() {Status = "ok"});
        }
        [HttpGet]
        [CheckPermission("House.Edit")]
        public ActionResult Edit(long id)
        {
            long adminUserId = (long)Session["LoginUserId"];
            long? cityId = AdminUserService.GetById(adminUserId).CityId;
            if (cityId == null)
            {
                return View("Error", (object)"总部人员不能管理房源");
            }
            var house = HouseService.GetById(id);
            var regions = RegionService.GetAll(cityId.Value);
            var roomTypes = IdNameService.GetAll("户型");
            var statuses = IdNameService.GetAll("房屋状态");
            var decorationStatuses = IdNameService.GetAll("装修状态");
            var types = IdNameService.GetAll("房屋类别");
            var attachments = AttachmentService.GetAll();
            var model = new HouseEditViewModel();
            model.Attachments = attachments;
            model.DecorateStatues = decorationStatuses;
            model.House = house;
            model.Regions = regions;
            model.RoomTypes = roomTypes;
            model.Statuses = statuses;
            model.Types = types;
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        [CheckPermission("House.Edit")]
        public ActionResult Edit(HouseEditPostModel model)
        {
            long adminUserId = (long)Session["LoginUserId"];
            long? cityId = AdminUserService.GetById(adminUserId).CityId;
            if (cityId == null)
            {
                return View("Error", (object)"总部人员不能管理房源");
            }
            var houseDTO = new HouseDTO();
            houseDTO.Id = model.Id;
            houseDTO.Address = model.HouseAddress;
            houseDTO.AttachmentIds = model.AttachmentIds;
            houseDTO.CheckInDateTime = model.CheckInDate;
            houseDTO.CommunityId = model.CommunityId;
            houseDTO.DecorateStatusId = model.DecorateStatusId;
            houseDTO.Description = model.Description;
            houseDTO.Direction = model.Direction;
            houseDTO.FloorIndex = model.FloorIndex;
            houseDTO.TotalFloorCount = model.TotalIndex;
            houseDTO.LookableDateTime = model.LookableDatetime;
            houseDTO.MonthRent = model.MonthRent;
            houseDTO.OwnerName = model.OwnerName;
            houseDTO.OwnerPhoneNum = model.OwnerPhoneNum;
            houseDTO.RoomTypeId = model.RoomTypeId;
            houseDTO.StatusId = model.StatusId;
            houseDTO.TotalFloorCount = model.TotalIndex;
            houseDTO.TypeId = model.TypeId;
            houseDTO.Area = model.HouseArea;
            HouseService.Update(houseDTO);
            return Json(new AjaxResult() { Status = "ok" });
        }
        public ActionResult LoadCommunities(long regionId)
        {
            var model = CommunityService.GetByRegionId(regionId);
            return Json(new AjaxResult() {Status = "ok", Data =  model});
        }
        [CheckPermission("House.PicList")]
        public ActionResult PicList(long id)
        {
            var model = HouseService.GetPics(id);
            return View(model);
        }
        [HttpPost]
        [CheckPermission("House.Delete")]
        public ActionResult Delete(long id)
        {
            return Json(new AjaxResult() { Status = "ok" });
        }
        [HttpPost]
        [CheckPermission("House.DeletePic")]
        public ActionResult DeletePic(long[] selectIds)
        {
            foreach(var picId in selectIds)
            {
                HouseService.DeleteHousePic(picId);
            }
            return Json(new AjaxResult() { Status = "ok" });
        }
        [HttpGet]
        [CheckPermission("House.PicUpload")]
        public ActionResult PicUpload(long houseId)
        {
            return View(houseId);
        }
        [HttpPost]
        [CheckPermission("House.PicUpload")]
        public ActionResult UploadPic(long houseId, HttpPostedFileBase file)
        {
            string md5 = CommonHelper.CalcMD5(file.InputStream);
            string ext = Path.GetExtension(file.FileName);
            string path = "/PicUpload/" + DateTime.Now.ToString("yyyy/MM/dd") + "/" + md5 + ext;
            string thumbPath = "/PicUpload/" + DateTime.Now.ToString("yyyy/MM/dd") + "/" + md5+"_thumb" + ext;
            string fullPath = Server.MapPath("~" + path);
            string fullThumbPath = Server.MapPath("~" + thumbPath);
            new FileInfo(fullPath).Directory.Create();//防止文件夹未创建
            file.InputStream.Position = 0;
            //这里有可能出现指针未复位的情况
            //file.SaveAs(fullPath);
            //添加缩略图
            ImageProcessingJob jobThumb = new ImageProcessingJob();
            jobThumb.Filters.Add(new FixedResizeConstraint(200, 200));
            jobThumb.SaveProcessedImageToFileSystem(file.InputStream, fullThumbPath);
            //指针复位
            //添加水印
            file.InputStream.Position = 0;
            ImageWatermark imgWatermark = new ImageWatermark(Server.MapPath("~/images/watermark.png"));
            imgWatermark.ContentAlignment = System.Drawing.ContentAlignment.BottomRight;
            imgWatermark.Alpha = 50;
            ImageProcessingJob jobWatermark = new ImageProcessingJob();
            jobWatermark.Filters.Add(imgWatermark);
            jobWatermark.Filters.Add(new FixedResizeConstraint(600, 600));
            jobWatermark.SaveProcessedImageToFileSystem(file.InputStream, fullPath);

            HouseService.AddNewHousePic(new HousePicDTO() { HouseId = houseId, Url = path, ThumbUrl = thumbPath });
            return Json(new AjaxResult() { Status = "ok" });
        }
    }
}