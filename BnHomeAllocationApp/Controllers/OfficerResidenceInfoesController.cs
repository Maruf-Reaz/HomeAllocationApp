using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BNHomeAllocation.Data;
using BnHomeAllocationApp.Models;
using BnHomeAllocationApp.Models.ViewModels;

namespace BnHomeAllocationApp.Controllers
{
    public class OfficerResidenceInfoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OfficerResidenceInfoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OfficerResidenceInfoes
        public async Task<IActionResult> Roster()
        {
            var officerResidenceInfos = await _context.OfficerResidenceInfoes
                .Include(o => o.Officer)
                .ThenInclude(o => o.OfficerRank)
                .Where(m => m.ApplicationTypeId == 1 && m.HasAppliedForResidence && !m.HasAppliedForResidenceChange && !m.IsAllotted)
                .ToListAsync();

            List<OfficerRosterViewModel> officerRosterViewModels = new List<OfficerRosterViewModel>();
            foreach (var officerResidenceInfo in officerResidenceInfos)
            {
                OfficerRosterViewModel officerRosterViewModel = new OfficerRosterViewModel();
                officerRosterViewModel.NameAndRank = officerResidenceInfo.Officer.OfficerRank.Name + " " + officerResidenceInfo.Officer.Name;
                officerRosterViewModel.PNO = officerResidenceInfo.Officer.PNO;
                officerRosterViewModel.OfficerId = officerResidenceInfo.Officer.Id;
                officerRosterViewModel.CommissionDate = officerResidenceInfo.Officer.CommissionDate;
                officerRosterViewModel.JoiningDate = officerResidenceInfo.JoiningDate;
                officerRosterViewModel.ApplicationDate = officerResidenceInfo.ApplicationDate;
                officerRosterViewModel.RankId = officerResidenceInfo.Officer.OfficerRankId;
                officerRosterViewModel.WorkPlace = officerResidenceInfo.Officer.CurrentWorkPlace;
                officerRosterViewModel.Preference = officerResidenceInfo.Preference;
                TimeSpan dateDiffService = officerRosterViewModel.ApplicationDate.Subtract(officerRosterViewModel.CommissionDate);
                TimeSpan dateDiffWaiting = DateTime.Now.Subtract(officerRosterViewModel.ApplicationDate);
                int servicePoint = Convert.ToInt32(dateDiffService.TotalDays / 365.25);
                decimal waitingPeriod = (Decimal)dateDiffWaiting.TotalDays / 30;
                int waiting = Convert.ToInt32(Math.Truncate(waitingPeriod));

                officerRosterViewModel.CommissionServicePoint = servicePoint;
                officerRosterViewModel.RankServicePoint = officerResidenceInfo.Officer.OfficerRank.RankPoint;
                officerRosterViewModel.WaitingPeriod = waiting;
                officerRosterViewModel.TotalPoint = officerRosterViewModel.CommissionServicePoint + officerRosterViewModel.RankServicePoint + officerRosterViewModel.WaitingPeriod;
                officerRosterViewModel.Position = 1;
                officerRosterViewModel.Children = officerResidenceInfo.Officer.NumberOfChildren;
                officerRosterViewModel.MobileNo = officerResidenceInfo.Officer.MobileNumber;
                officerRosterViewModel.Preference = officerResidenceInfo.Preference;
                officerRosterViewModel.Remarks = officerResidenceInfo.Remarks;

                officerRosterViewModels.Add(officerRosterViewModel);

            }
            var finalList = officerRosterViewModels.OrderByDescending(item => item.TotalPoint);
            ViewData["Ranks"] = await _context.OfficerRanks
                .ToListAsync();
            ViewData["OfficerResidenceInfo"] = finalList;
            return View();
        }




        public async Task<IActionResult> SpecialConsideration()
        {
            var officerResidenceInfos = await _context.OfficerResidenceInfoes
                .Include(o => o.Officer)
                .ThenInclude(o => o.OfficerRank)
                .Where(m => m.ApplicationTypeId == 2 && m.HasAppliedForResidence && !m.HasAppliedForResidenceChange && !m.IsAllotted)
                .ToListAsync();

            List<OfficerRosterViewModel> officerRosterViewModels = new List<OfficerRosterViewModel>();
            foreach (var officerResidenceInfo in officerResidenceInfos)
            {
                OfficerRosterViewModel officerRosterViewModel = new OfficerRosterViewModel();
                officerRosterViewModel.NameAndRank = officerResidenceInfo.Officer.OfficerRank.Name + " " + officerResidenceInfo.Officer.Name;
                officerRosterViewModel.PNO = officerResidenceInfo.Officer.PNO;
                officerRosterViewModel.Preference = officerResidenceInfo.Preference;
                officerRosterViewModel.OfficerId = officerResidenceInfo.Officer.Id;
                officerRosterViewModel.CommissionDate = officerResidenceInfo.Officer.CommissionDate;
                officerRosterViewModel.JoiningDate = officerResidenceInfo.JoiningDate;
                officerRosterViewModel.ApplicationDate = officerResidenceInfo.ApplicationDate;
                officerRosterViewModel.RankId = officerResidenceInfo.Officer.OfficerRankId;
                officerRosterViewModel.WorkPlace = officerResidenceInfo.Officer.CurrentWorkPlace;
                TimeSpan dateDiffService = officerRosterViewModel.ApplicationDate.Subtract(officerRosterViewModel.CommissionDate);
                TimeSpan dateDiffWaiting = DateTime.Now.Subtract(officerRosterViewModel.ApplicationDate);
                int servicePoint = Convert.ToInt32(dateDiffService.TotalDays / 365.25);
                decimal waitingPeriod = (Decimal)dateDiffWaiting.TotalDays / 30;
                int waiting = Convert.ToInt32(Math.Truncate(waitingPeriod));

                officerRosterViewModel.CommissionServicePoint = servicePoint;
                officerRosterViewModel.RankServicePoint = officerResidenceInfo.Officer.OfficerRank.RankPoint;
                officerRosterViewModel.WaitingPeriod = waiting;
                officerRosterViewModel.TotalPoint = officerRosterViewModel.CommissionServicePoint + officerRosterViewModel.RankServicePoint + officerRosterViewModel.WaitingPeriod;
                officerRosterViewModel.Position = 1;
                officerRosterViewModel.Children = officerResidenceInfo.Officer.NumberOfChildren;
                officerRosterViewModel.MobileNo = officerResidenceInfo.Officer.MobileNumber;
                officerRosterViewModel.Preference = officerResidenceInfo.Preference;
                officerRosterViewModel.Remarks = officerResidenceInfo.Remarks;

                officerRosterViewModels.Add(officerRosterViewModel);

            }
            var finalList = officerRosterViewModels.OrderByDescending(item => item.TotalPoint);
            ViewData["Ranks"] = await _context.OfficerRanks
                .ToListAsync();
            ViewData["OfficerResidenceInfo"] = finalList;
            return View();
        }



        public async Task<IActionResult> NAM()
        {
            var officerResidenceInfos = await _context.OfficerResidenceInfoes
                .Include(o => o.Officer)
                .ThenInclude(o => o.OfficerRank)
                .Where(m => m.ApplicationTypeId == 3 && m.HasAppliedForResidence && !m.HasAppliedForResidenceChange && !m.IsAllotted)
                .ToListAsync();

            List<OfficerRosterViewModel> officerRosterViewModels = new List<OfficerRosterViewModel>();
            foreach (var officerResidenceInfo in officerResidenceInfos)
            {
                OfficerRosterViewModel officerRosterViewModel = new OfficerRosterViewModel();
                officerRosterViewModel.NameAndRank = officerResidenceInfo.Officer.OfficerRank.Name + " " + officerResidenceInfo.Officer.Name;
                officerRosterViewModel.PNO = officerResidenceInfo.Officer.PNO;
                officerRosterViewModel.OfficerId = officerResidenceInfo.Officer.Id;
                officerRosterViewModel.Preference = officerResidenceInfo.Preference;
                officerRosterViewModel.CommissionDate = officerResidenceInfo.Officer.CommissionDate;
                officerRosterViewModel.JoiningDate = officerResidenceInfo.JoiningDate;
                officerRosterViewModel.ApplicationDate = officerResidenceInfo.ApplicationDate;
                officerRosterViewModel.RankId = officerResidenceInfo.Officer.OfficerRankId;
                officerRosterViewModel.WorkPlace = officerResidenceInfo.Officer.CurrentWorkPlace;
                TimeSpan dateDiffService = officerRosterViewModel.ApplicationDate.Subtract(officerRosterViewModel.CommissionDate);
                TimeSpan dateDiffWaiting = DateTime.Now.Subtract(officerRosterViewModel.ApplicationDate);
                int servicePoint = Convert.ToInt32(dateDiffService.TotalDays / 365.25);
                decimal waitingPeriod = (Decimal)dateDiffWaiting.TotalDays / 30;
                int waiting = Convert.ToInt32(Math.Truncate(waitingPeriod));

                officerRosterViewModel.CommissionServicePoint = servicePoint;
                officerRosterViewModel.RankServicePoint = officerResidenceInfo.Officer.OfficerRank.RankPoint;
                officerRosterViewModel.WaitingPeriod = waiting;
                officerRosterViewModel.TotalPoint = officerRosterViewModel.CommissionServicePoint + officerRosterViewModel.RankServicePoint + officerRosterViewModel.WaitingPeriod;
                officerRosterViewModel.Position = 1;
                officerRosterViewModel.Children = officerResidenceInfo.Officer.NumberOfChildren;
                officerRosterViewModel.MobileNo = officerResidenceInfo.Officer.MobileNumber;
                officerRosterViewModel.Preference = officerResidenceInfo.Preference;
                officerRosterViewModel.Remarks = officerResidenceInfo.Remarks;

                officerRosterViewModels.Add(officerRosterViewModel);

            }
            var finalList = officerRosterViewModels.OrderByDescending(item => item.TotalPoint);
            ViewData["Ranks"] = await _context.OfficerRanks
                .ToListAsync();
            ViewData["OfficerResidenceInfo"] = finalList;
            return View();
        }


        public IActionResult Allotte(int id)
        {

            var officer = _context.Officers
               .Include(o => o.OfficerRank)
               .Where(m => m.Id == id)
               .FirstOrDefault();
            var residenceZones = _context.ResidenceZones
               .ToList();

            ViewData["Officer"] = officer;
            ViewData["ResidenceZones"] = new SelectList(residenceZones, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Allotte(OfficerResidenceInfo officerResidenceInfo)
        {

            if (ModelState.IsValid)
            {
                var tempOfficerResidenceInfo = _context.OfficerResidenceInfoes
               .Include(o => o.Officer)
               .Where(m => m.OfficerId == officerResidenceInfo.OfficerId && m.HasAppliedForResidence)
               .FirstOrDefault();

                tempOfficerResidenceInfo.AllottementDate = officerResidenceInfo.AllottementDate;
                tempOfficerResidenceInfo.Preference = "";
                tempOfficerResidenceInfo.Remarks = "";
                tempOfficerResidenceInfo.HasAppliedForResidence = false;
                tempOfficerResidenceInfo.HasAppliedForResidenceChange = false;
                tempOfficerResidenceInfo.IsAllotted = true;
                tempOfficerResidenceInfo.ResidenceId = officerResidenceInfo.ResidenceId;


                var residence = _context.Residences
               .Where(m => m.Id == officerResidenceInfo.ResidenceId)
               .FirstOrDefault();

                residence.IsAllotted = true;

                _context.Update(residence);
                _context.Update(tempOfficerResidenceInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Roster));
            }
            var officer = _context.Officers
                          .Include(o => o.OfficerRank)
                          .Where(m => m.Id == officerResidenceInfo.OfficerId)
                          .FirstOrDefault();
            var residenceZones = _context.ResidenceZones

               .ToList();

            ViewData["Officer"] = officer;
            ViewData["ResidenceZones"] = new SelectList(residenceZones, "Id", "Name");
            return View();
        }




        public IActionResult GeneralAllotte()
        {
            var residenceZones = _context.ResidenceZones
               .ToList();
            var ranks = _context.OfficerRanks
               .ToList();

            ViewData["Ranks"] = new SelectList(ranks, "Id", "Name");

            ViewData["ResidenceZones"] = new SelectList(residenceZones, "Id", "Name");
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> GeneralAllotte(OfficerResidenceInfo officerResidenceInfo)
        {
            if (ModelState.IsValid)
            {
                OfficerResidenceInfo tempOfficerResidenceInfo = new OfficerResidenceInfo();
                tempOfficerResidenceInfo.OfficerId = officerResidenceInfo.OfficerId;
                tempOfficerResidenceInfo.AllottementDate = officerResidenceInfo.AllottementDate;
                tempOfficerResidenceInfo.Preference = "";
                tempOfficerResidenceInfo.Remarks = "";
                tempOfficerResidenceInfo.HasAppliedForResidence = false;
                tempOfficerResidenceInfo.HasAppliedForResidenceChange = false;
                tempOfficerResidenceInfo.IsAllotted = true;
                tempOfficerResidenceInfo.ResidenceId = officerResidenceInfo.ResidenceId;

                var residence = _context.Residences
               .Where(m => m.Id == officerResidenceInfo.ResidenceId)
               .FirstOrDefault();
                residence.IsAllotted = true;

                _context.Add(tempOfficerResidenceInfo);
                _context.Update(residence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AllottementState));
            }
            var residenceZones = _context.ResidenceZones
                .ToList();
            var ranks = _context.OfficerRanks
               .ToList();

            ViewData["Ranks"] = new SelectList(ranks, "Id", "Name");

            ViewData["ResidenceZones"] = new SelectList(residenceZones, "Id", "Name");
            return View();
        }





        public async Task<IActionResult> AllottementState()
        {

            var officerResidenceInfos = await _context.OfficerResidenceInfoes
                .Include(o => o.Residence)
                .Include(o => o.Officer)
                .ThenInclude(o => o.OfficerRank)
                .Where(m => m.IsAllotted)
                .ToListAsync();

            //List<OfficerRosterViewModel> officerRosterViewModels = new List<OfficerRosterViewModel>();
            //foreach (var officerResidenceInfo in officerResidenceInfos)
            //{
            //    OfficerRosterViewModel officerRosterViewModel = new OfficerRosterViewModel();
            //    officerRosterViewModel.NameAndRank = officerResidenceInfo.Officer.Name + " " + officerResidenceInfo.Officer.OfficerRank.Name;
            //    officerRosterViewModel.PNO = officerResidenceInfo.Officer.PNO;
            //    officerRosterViewModel.OfficerId = officerResidenceInfo.Officer.Id;
            //    officerRosterViewModel.CommissionDate = officerResidenceInfo.Officer.CommissionDate;
            //    officerRosterViewModel.JoiningDate = officerResidenceInfo.JoiningDate;
            //    officerRosterViewModel.ApplicationDate = officerResidenceInfo.ApplicationDate;
            //    officerRosterViewModel.RankId = officerResidenceInfo.Officer.OfficerRankId;
            //    officerRosterViewModel.WorkPlace = officerResidenceInfo.Officer.CurrentWorkPlace;
            //    TimeSpan dateDiffService = officerRosterViewModel.ApplicationDate.Subtract(officerRosterViewModel.CommissionDate);
            //    TimeSpan dateDiffWaiting = DateTime.Now.Subtract(officerRosterViewModel.ApplicationDate);
            //    int servicePoint = Convert.ToInt32(dateDiffService.TotalDays / 365.25);
            //    decimal waitingPeriod = (Decimal)dateDiffWaiting.TotalDays / 30;
            //    int waiting = Convert.ToInt32(Math.Truncate(waitingPeriod));

            //    officerRosterViewModel.CommissionServicePoint = servicePoint;
            //    officerRosterViewModel.RankServicePoint = officerResidenceInfo.Officer.OfficerRank.RankPoint;
            //    officerRosterViewModel.WaitingPeriod = waiting;
            //    officerRosterViewModel.TotalPoint = officerRosterViewModel.CommissionServicePoint + officerRosterViewModel.RankServicePoint + officerRosterViewModel.WaitingPeriod;
            //    officerRosterViewModel.Position = 1;
            //    officerRosterViewModel.Children = officerResidenceInfo.Officer.NumberOfChildren;
            //    officerRosterViewModel.MobileNo = officerResidenceInfo.Officer.MobileNumber;
            //    officerRosterViewModel.Preference = officerResidenceInfo.Preference;
            //    officerRosterViewModel.Remarks = officerResidenceInfo.Remarks;

            //    officerRosterViewModels.Add(officerRosterViewModel);

            //}
            var finalList = officerResidenceInfos.OrderByDescending(item => item.Officer.OfficerRankId);
            ViewData["Ranks"] = await _context.OfficerRanks
                .ToListAsync();
            ViewData["OfficerResidenceInfo"] = finalList;
            return View();
        }

        public IActionResult ApplyForChange()
        {

            var ranks = _context.OfficerRanks
                .ToList();
            ViewData["Ranks"] = new SelectList(ranks, "Id", "Name");

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> ApplyForChange(OfficerResidenceInfo officerResidenceInfo)
        {
            if (ModelState.IsValid)
            {
                var tempOfficerResidenceInfo = await _context.OfficerResidenceInfoes.Where(m => m.Id == officerResidenceInfo.Id).FirstOrDefaultAsync();
                tempOfficerResidenceInfo.ApplicationTypeId = officerResidenceInfo.ApplicationTypeId;
                tempOfficerResidenceInfo.ApplicationDate = officerResidenceInfo.ApplicationDate;
                tempOfficerResidenceInfo.JoiningDate = officerResidenceInfo.JoiningDate;
                tempOfficerResidenceInfo.Reason = officerResidenceInfo.Reason;
                tempOfficerResidenceInfo.Remarks = officerResidenceInfo.Remarks;
                tempOfficerResidenceInfo.HasAppliedForResidence = false;
                tempOfficerResidenceInfo.IsAllotted = true;
                tempOfficerResidenceInfo.HasAppliedForResidenceChange = true;

                _context.Update(tempOfficerResidenceInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ApplicationsForChange));
            }

            var ranks = _context.OfficerRanks
                .ToList();
            ViewData["Ranks"] = new SelectList(ranks, "Id", "Name");

            return View();

        }

        public async Task<IActionResult> ApplicationsForChange()
        {

            var officerResidenceInfos = await _context.OfficerResidenceInfoes
                .Include(o => o.Officer)
                .ThenInclude(o => o.OfficerRank)
                .Where(m => m.ApplicationTypeId == 1 && m.HasAppliedForResidenceChange && m.IsAllotted)
                .ToListAsync();

            List<OfficerRosterViewModel> officerRosterViewModels = new List<OfficerRosterViewModel>();
            foreach (var officerResidenceInfo in officerResidenceInfos)
            {
                OfficerRosterViewModel officerRosterViewModel = new OfficerRosterViewModel();


                officerRosterViewModel.NameAndRank = officerResidenceInfo.Officer.OfficerRank.Name + " " + officerResidenceInfo.Officer.Name;
                officerRosterViewModel.Id = officerResidenceInfo.Id;
                officerRosterViewModel.PNO = officerResidenceInfo.Officer.PNO;
                officerRosterViewModel.OfficerId = officerResidenceInfo.Officer.Id;
                officerRosterViewModel.CommissionDate = officerResidenceInfo.Officer.CommissionDate;
                officerRosterViewModel.JoiningDate = officerResidenceInfo.JoiningDate;
                officerRosterViewModel.ApplicationDate = officerResidenceInfo.ApplicationDate;
                officerRosterViewModel.RankId = officerResidenceInfo.Officer.OfficerRankId;
                officerRosterViewModel.WorkPlace = officerResidenceInfo.Officer.CurrentWorkPlace;
                TimeSpan dateDiffService = officerRosterViewModel.ApplicationDate.Subtract(officerRosterViewModel.CommissionDate);
                TimeSpan dateDiffWaiting = DateTime.Now.Subtract(officerRosterViewModel.ApplicationDate);
                int servicePoint = Convert.ToInt32(dateDiffService.TotalDays / 365.25);
                decimal waitingPeriod = (Decimal)dateDiffWaiting.TotalDays / 30;
                int waiting = Convert.ToInt32(Math.Truncate(waitingPeriod));

                officerRosterViewModel.CommissionServicePoint = servicePoint;
                officerRosterViewModel.RankServicePoint = officerResidenceInfo.Officer.OfficerRank.RankPoint;
                officerRosterViewModel.WaitingPeriod = waiting;
                officerRosterViewModel.TotalPoint = officerRosterViewModel.CommissionServicePoint + officerRosterViewModel.RankServicePoint + officerRosterViewModel.WaitingPeriod;
                officerRosterViewModel.Position = 1;
                officerRosterViewModel.Children = officerResidenceInfo.Officer.NumberOfChildren;
                officerRosterViewModel.MobileNo = officerResidenceInfo.Officer.MobileNumber;
                officerRosterViewModel.Preference = officerResidenceInfo.Preference;
                officerRosterViewModel.Remarks = officerResidenceInfo.Remarks;
                officerRosterViewModel.Reason = officerResidenceInfo.Reason;

                officerRosterViewModels.Add(officerRosterViewModel);

            }
            var finalList = officerRosterViewModels.OrderByDescending(item => item.TotalPoint);
            ViewData["Ranks"] = await _context.OfficerRanks
                .ToListAsync();
            ViewData["OfficerResidenceInfo"] = finalList;
            return View();
        }


        public async Task<IActionResult> ProbableVacancy()
        {

            var residenceBuildings = await _context.ResidenceBuildings
                .ToListAsync();
            var officerResidenceInfos = await _context.OfficerResidenceInfoes
             .Include(o => o.Officer)
             .ThenInclude(o => o.OfficerRank)
             .Include(o => o.Residence)
             .ThenInclude(o => o.ResidenceBuilding)
             .ThenInclude(o => o.BuildingType)
             .Where(m => m.IsAllotted)
             .ToListAsync();

            ViewData["OfficerResidenceInfo"] = officerResidenceInfos;
            ViewData["ResidenceBuildings"] = residenceBuildings;

            return View();
        }

        public async Task<IActionResult> ProbableVacancyList()
        {

            var residenceBuildings = await _context.ResidenceBuildings
                .ToListAsync();
            var officerResidenceInfos = await _context.OfficerResidenceInfoes
             .Include(o => o.Officer)
             .ThenInclude(o => o.OfficerRank)
             .Include(o => o.Residence)
             .ThenInclude(o => o.ResidenceBuilding)
             .ThenInclude(o => o.BuildingType)
             .Where(m => m.IsVancancyProbable)
             .ToListAsync();

            ViewData["OfficerResidenceInfo"] = officerResidenceInfos;
            ViewData["ResidenceBuildings"] = residenceBuildings;

            return View();
        }



        public IActionResult AddProbableVacancy(int? id)
        {

            var officerResidenceInfo = _context.OfficerResidenceInfoes
                .Include(m => m.Officer)
                .ThenInclude(m => m.OfficerRank)
                .Include(m => m.Residence)
                .ThenInclude(m => m.ResidenceBuilding)
                .ThenInclude(m => m.BuildingType)
                .Where(m => m.Id == id)
               .FirstOrDefault();

            ViewData["OfficerResidenceInfo"] = officerResidenceInfo;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddProbableVacancy(OfficerResidenceInfo officerResidenceInfo)
        {
            if (ModelState.IsValid)
            {
                var tempOfficerResidenceInfo = await _context.OfficerResidenceInfoes.Where(m => m.Id == officerResidenceInfo.Id).FirstOrDefaultAsync();


                tempOfficerResidenceInfo.ProbableVacancyDate = officerResidenceInfo.ProbableVacancyDate;
                tempOfficerResidenceInfo.VacancyReason = officerResidenceInfo.VacancyReason;
                tempOfficerResidenceInfo.Remarks = officerResidenceInfo.Remarks;
                tempOfficerResidenceInfo.IsVancancyProbable = true;

                _context.Update(tempOfficerResidenceInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ProbableVacancyList));
            }

            var officerResidenceInfoTemp = _context.OfficerResidenceInfoes
               .Include(m => m.Officer)
               .ThenInclude(m => m.OfficerRank)
               .Include(m => m.Residence)
               .ThenInclude(m => m.ResidenceBuilding)
               .ThenInclude(m => m.BuildingType)
               .Where(m => m.Id == officerResidenceInfo.Id)
              .ToList();

            ViewData["OfficerResidenceInfo"] = officerResidenceInfoTemp;

            return View();

        }



        public IActionResult ChangeResidence(int? id)
        {

            var officerResidenceInfo = _context.OfficerResidenceInfoes
               .Include(m => m.Officer)
               .ThenInclude(m => m.OfficerRank)
               .Include(m => m.Residence)
               .ThenInclude(m => m.ResidenceBuilding)
               .ThenInclude(m => m.BuildingType)
               .Where(m => m.Id == id)
              .FirstOrDefault();
            var residenceZones = _context.ResidenceZones
               .ToList();


            ViewData["ResidenceZones"] = new SelectList(residenceZones, "Id", "Name");
            ViewData["OfficerResidenceInfo"] = officerResidenceInfo;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeResidence(OfficerResidenceInfo officerResidenceInfo)
        {




            if (ModelState.IsValid)
            {
                var previousInfo = await _context.OfficerResidenceInfoes.Where(m => m.Id == officerResidenceInfo.Id).FirstOrDefaultAsync();

                var previousResidence = await _context.Residences.Where(m => m.Id == previousInfo.ResidenceId).FirstOrDefaultAsync();

                previousResidence.IsAllotted = false;
                _context.Remove(previousInfo);
                _context.Update(previousResidence);
                await _context.SaveChangesAsync();


                OfficerResidenceInfo tempOfficerResidenceInfo = new OfficerResidenceInfo();
                tempOfficerResidenceInfo.OfficerId = officerResidenceInfo.OfficerId;
                tempOfficerResidenceInfo.AllottementDate = officerResidenceInfo.AllottementDate;
                tempOfficerResidenceInfo.ApplicationTypeId = officerResidenceInfo.ApplicationTypeId;
                tempOfficerResidenceInfo.Preference = "";
                tempOfficerResidenceInfo.Remarks = "";
                tempOfficerResidenceInfo.HasAppliedForResidence = false;
                tempOfficerResidenceInfo.HasAppliedForResidenceChange = false;
                tempOfficerResidenceInfo.IsAllotted = true;
                tempOfficerResidenceInfo.ResidenceId = officerResidenceInfo.ResidenceId;

                var residence = _context.Residences
               .Where(m => m.Id == officerResidenceInfo.ResidenceId)
               .FirstOrDefault();
                residence.IsAllotted = true;

                _context.Add(tempOfficerResidenceInfo);
                _context.Update(residence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ApplicationsForChange));
            }

            var finalOfficerResidenceInfo = _context.OfficerResidenceInfoes
               .Include(m => m.Officer)
               .ThenInclude(m => m.OfficerRank)
               .Include(m => m.Residence)
               .ThenInclude(m => m.ResidenceBuilding)
               .ThenInclude(m => m.BuildingType)
               .Where(m => m.Id == officerResidenceInfo.Id)
              .FirstOrDefault();
            var residenceZones = _context.ResidenceZones
               .ToList();


            ViewData["ResidenceZones"] = new SelectList(residenceZones, "Id", "Name");
            ViewData["OfficerResidenceInfo"] = finalOfficerResidenceInfo;

            return View();
        }


        public async Task<IActionResult> VacantResidence()
        {

            var officerResidenceInfos = await _context.OfficerResidenceInfoes
               .Include(o => o.Residence)
               .Include(o => o.Officer)
               .ThenInclude(o => o.OfficerRank)
               .Where(m => m.IsAllotted)
               .ToListAsync();


            var finalList = officerResidenceInfos.OrderByDescending(item => item.Officer.OfficerRankId);
            ViewData["Ranks"] = await _context.OfficerRanks
                .ToListAsync();
            ViewData["OfficerResidenceInfo"] = finalList;
            return View();
        }




        public async Task<IActionResult> VacantCertainResidence(int? id)
        {


            var previousInfo = await _context.OfficerResidenceInfoes.Where(m => m.Id == id).FirstOrDefaultAsync();

            var previousResidence = await _context.Residences.Where(m => m.Id == previousInfo.ResidenceId).FirstOrDefaultAsync();

            previousResidence.IsAllotted = false;
            _context.Remove(previousInfo);
            _context.Update(previousResidence);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(VacantResidence));


        }






        public IActionResult ApplyForAllotte()
        {
            ViewData["Ranks"] = new SelectList(_context.OfficerRanks, "Id", "Name");
            ViewData["ApplicationType"] = new SelectList(_context.ApplicationTypes, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ApplyForAllotte(OfficerResidenceInfo officerResidenceInfo)
        {

            if (ModelState.IsValid)
            {
                OfficerResidenceInfo tempOfficerResidenceInfo = new OfficerResidenceInfo();


                tempOfficerResidenceInfo.OfficerId = officerResidenceInfo.OfficerId;
                tempOfficerResidenceInfo.ApplicationTypeId = officerResidenceInfo.ApplicationTypeId;
                tempOfficerResidenceInfo.ApplicationDate = officerResidenceInfo.ApplicationDate;
                tempOfficerResidenceInfo.JoiningDate = officerResidenceInfo.JoiningDate;
                tempOfficerResidenceInfo.Preference = officerResidenceInfo.Preference;
                tempOfficerResidenceInfo.Remarks = officerResidenceInfo.Remarks;
                tempOfficerResidenceInfo.HasAppliedForResidence = true;

                _context.Add(tempOfficerResidenceInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Roster));
            }

            ViewData["Ranks"] = new SelectList(_context.OfficerRanks, "Id", "Name");
            ViewData["ApplicationType"] = new SelectList(_context.ApplicationTypes, "Id", "Name");
            return View();
        }






        // GET: OfficerResidenceInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officerResidenceInfo = await _context.OfficerResidenceInfoes
                .Include(o => o.Officer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (officerResidenceInfo == null)
            {
                return NotFound();
            }

            return View(officerResidenceInfo);
        }

        // GET: OfficerResidenceInfoes/Create
        public IActionResult Create()
        {
            ViewData["OfficerId"] = new SelectList(_context.Officers, "Id", "Id");
            return View();
        }

        // POST: OfficerResidenceInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OfficerResidenceInfo officerResidenceInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(officerResidenceInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Roster));
            }
            ViewData["OfficerId"] = new SelectList(_context.Officers, "Id", "Id", officerResidenceInfo.OfficerId);
            return View(officerResidenceInfo);
        }

        // GET: OfficerResidenceInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officerResidenceInfo = await _context.OfficerResidenceInfoes.FindAsync(id);
            if (officerResidenceInfo == null)
            {
                return NotFound();
            }
            ViewData["OfficerId"] = new SelectList(_context.Officers, "Id", "Id", officerResidenceInfo.OfficerId);
            return View(officerResidenceInfo);
        }

        // POST: OfficerResidenceInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Rank,OfficerId,IsAllotted,HasAppliedForResidence,HasAppliedForResidenceChange,ApplicationDate")] OfficerResidenceInfo officerResidenceInfo)
        {
            if (id != officerResidenceInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(officerResidenceInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfficerResidenceInfoExists(officerResidenceInfo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Roster));
            }
            ViewData["OfficerId"] = new SelectList(_context.Officers, "Id", "Id", officerResidenceInfo.OfficerId);
            return View(officerResidenceInfo);
        }

        // GET: OfficerResidenceInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officerResidenceInfo = await _context.OfficerResidenceInfoes
                .Include(o => o.Officer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (officerResidenceInfo == null)
            {
                return NotFound();
            }

            return View(officerResidenceInfo);
        }

        // POST: OfficerResidenceInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var officerResidenceInfo = await _context.OfficerResidenceInfoes.FindAsync(id);
            _context.OfficerResidenceInfoes.Remove(officerResidenceInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Roster));
        }

        private bool OfficerResidenceInfoExists(int id)
        {
            return _context.OfficerResidenceInfoes.Any(e => e.Id == id);

        }


        [HttpPost]
        public async Task<JsonResult> GetUnAllottedOfficers(int? id)
        {
            if (id == null)
            {
                return Json(false);
            }
            var officers = await _context.Officers
               .Where(m => m.OfficerRankId == id)
               .ToListAsync();
            var allottedOfficers = await _context.OfficerResidenceInfoes
               .Where(m => m.IsAllotted && m.Officer.OfficerRankId == id)
               .ToListAsync();

            List<Officer> tempOfficers = new List<Officer>();
            foreach (var allottedOfficer in allottedOfficers)
            {
                var tempOfficer = await _context.Officers.Where(m => m.Id == allottedOfficer.OfficerId)
                    .FirstOrDefaultAsync();

                tempOfficers.Add(tempOfficer);
            }


            List<Officer> result = officers.Except(tempOfficers).ToList();


            if (result == null)
            {
                return Json(false);
            }
            return Json(result);
        }
        [HttpPost]
        public async Task<JsonResult> GetAllottedOfficers(int? id)
        {
            if (id == null)
            {
                return Json(false);
            }
            var officers = await _context.Officers
               .Where(m => m.OfficerRankId == id)
               .ToListAsync();
            var allottedOfficers = await _context.OfficerResidenceInfoes
                .Include(m => m.Officer)
               .Where(m => m.IsAllotted && !m.HasAppliedForResidenceChange && !m.HasAppliedForResidence && m.Officer.OfficerRankId == id)
               .ToListAsync();


            if (allottedOfficers == null)
            {
                return Json(false);
            }
            return Json(allottedOfficers);
        }


        [HttpPost]
        public async Task<JsonResult> GetAllottedOfficerById(int? id)
        {
            if (id == null)
            {
                return Json(false);
            }


            var allottedOfficer = await _context.OfficerResidenceInfoes
                .Include(m => m.Officer)
               .Where(m => m.IsAllotted && !m.HasAppliedForResidenceChange && !m.HasAppliedForResidence && m.Officer.Id == id)
               .FirstOrDefaultAsync();


            if (allottedOfficer == null)
            {
                return Json(false);
            }
            return Json(allottedOfficer);
        }


    }
}
