using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibertyFamilySystem.Models;
using LibertyFamilySystem.Repositories;
using LibertyFamilySystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LibertyFamilySystem.Controllers
{
    
    public class AdministrationController : Controller
    {
        private IUserRepository _userRepository;
        private IMemberRepository _memberRepository;
        private IAttendanceRepository _attendanceRepository;
        private IEventRepository _eventRepository;
        private IDashboardRepository _dashboardRepository;
        private IGroupRepository _groupRepository;

        public AdministrationController(IUserRepository userRepository, IMemberRepository memberRepository,
            IAttendanceRepository attendanceRepository, IEventRepository eventRepository, 
            IDashboardRepository dashboardRepository, IGroupRepository groupRepository)
        {
            _userRepository = userRepository;
            _memberRepository = memberRepository;
            _attendanceRepository = attendanceRepository;
            _eventRepository = eventRepository;
            _dashboardRepository = dashboardRepository;
            _groupRepository = groupRepository;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser()
        {
            IEnumerable<UserViewModel> model = await _userRepository.GetUsers();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<PartialViewResult> _AddEditUser(string status, string username)
        {
            if (status == "add")
            {
                var user = await _userRepository.LoadUsers(status, username);
                ViewBag.status = "add";
                return PartialView("~/PartialViews/Users/_AddEditUser.cshtml",user);
            }
            else if (status == "update" && (username != null))
            {
                ViewBag.status = "update";
                var user = await _userRepository.LoadUsers(status, username);
                return PartialView("~/PartialViews/Users/_AddEditUser.cshtml",user);
            }

            return PartialView();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> _AddUser([FromBody] UserViewModel userV)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new KeyValuePair<bool, string>(false, GetModelStateErrors(ModelState)));
                }

                var result = await _userRepository.Add(userV);

                return Json(result);
            }
            catch (Exception ex)
            {

                return Json(new KeyValuePair<bool, string>(false, ex.Message));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> _UpdateUser([FromBody] UserViewModel userV)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new KeyValuePair<bool, string>(false, GetModelStateErrors(ModelState)));
                }

                var result = await _userRepository.Update(userV);

                return Json(result);
            }
            catch (Exception ex)
            {

                return Json(new KeyValuePair<bool, string>(false, ex.Message));
            }
        }

        [Authorize(Roles = "Admin, GroupAdmin")]
        public async Task<IActionResult> GetMembers(string groupId = "")
        {
            IEnumerable<Member> members = new List<Member>();
            if(!string.IsNullOrWhiteSpace(groupId))
                members =  _memberRepository.GetMemberByGroup(Convert.ToInt32(groupId));
            else
                members = await _memberRepository.GetMembers();

            return View(members);
        }

        [Authorize(Roles = "Admin, GroupAdmin")]
        public async Task<PartialViewResult> _AddEditMember(string status, int memberId)
        {
            if (status == "add")
            {
                var user = await _memberRepository.LoadMembers(status, memberId);
                ViewBag.status = "add";
                return PartialView("~/PartialViews/Members/_AddEditMember.cshtml", user);
            }
            else if (status == "update" && (memberId != null))
            {
                ViewBag.status = "update";
                var user = await _memberRepository.LoadMembers(status, memberId);
                return PartialView("~/PartialViews/Members/_AddEditMember.cshtml", user);
            }

            return PartialView();
        }

        [Authorize(Roles = "Admin, GroupAdmin")]
        public async Task<IActionResult> _AddMember(Member member)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new KeyValuePair<bool, string>(false, GetModelStateErrors(ModelState)));
                }

                var result = await _memberRepository.Add(member);

                return Json(result);
            }
            catch (Exception ex)
            {

                return Json(new KeyValuePair<bool, string>(false, ex.Message));
            }
        }

        [Authorize(Roles = "Admin, GroupAdmin")]
        [HttpPost]
        public async Task<IActionResult> _UpdateMember(Member member)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new KeyValuePair<bool, string>(false, GetModelStateErrors(ModelState)));
                }

                var result = await _memberRepository.Update(member);

                return Json(result);
            }
            catch (Exception ex)
            {

                return Json(new KeyValuePair<bool, string>(false, ex.Message));
            }
        }

        [Authorize(Roles = "Admin, GroupAdmin")]
        [HttpGet]
        public async Task<IActionResult> GetAttendanceSheet(int groupId)
        {
            try
            {
                ViewBag.ActiveEvent = _eventRepository.GetActiveEvent().Name;
                IEnumerable<AttendanceViewModel> attendanceV = new List<AttendanceViewModel>();
                attendanceV = await _attendanceRepository.GetAttendanceSheet(groupId);   
                return View(attendanceV);
            }
            catch (Exception ex)
            {

                return Json(new KeyValuePair<bool, string>(false, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> MarkAttendance(int memberId)
        {
            try
            {
               var result =  await _attendanceRepository.MarkAttendace(memberId);
                return Json(result);
            }
            catch (Exception ex)
            {

                return Json(new KeyValuePair<bool, string>(false, ex.Message));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            var result = _dashboardRepository.GetStatistics();
            return View(result);
        }

        [Authorize(Roles = "Admin, GroupAdmin")]
        public IActionResult GetGroups(string groupId = "")
        {
            IEnumerable<Group> groups = new List<Group>();
            groups =  _groupRepository.GetGroup();
            return View(groups);
        }

        [Authorize(Roles = "Admin, GroupAdmin")]
        public PartialViewResult _AddEditGroup(string status, int groupId)
        {
            if (status == "add")
            {
                var user =  _groupRepository.LoadGroups(status, groupId);
                ViewBag.status = "add";
                return PartialView("~/PartialViews/Groups/_AddEditGroup.cshtml", user);
            }
            else if (status == "update" && (groupId != null))
            {
                ViewBag.status = "update";
                var user =  _groupRepository.LoadGroups(status, groupId);
                return PartialView("~/PartialViews/Groups/_AddEditGroup.cshtml", user);
            }

            return PartialView();
        }

        [Authorize(Roles = "Admin, GroupAdmin")]
        public async Task<IActionResult> _AddGroup(Group group)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new KeyValuePair<bool, string>(false, GetModelStateErrors(ModelState)));
                }

                var result = await _groupRepository.Add(group);

                return Json(result);
            }
            catch (Exception ex)
            {

                return Json(new KeyValuePair<bool, string>(false, ex.Message));
            }
        }

        [Authorize(Roles = "Admin, GroupAdmin")]
        [HttpPost]
        public async Task<IActionResult> _UpdateGroup(Group group)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new KeyValuePair<bool, string>(false, GetModelStateErrors(ModelState)));
                }

                var result = await _groupRepository.Update(group);

                return Json(result);
            }
            catch (Exception ex)
            {

                return Json(new KeyValuePair<bool, string>(false, ex.Message));
            }
        }


        private string GetModelStateErrors(ModelStateDictionary modelState)
        {
            string error = "";

            foreach (var item in modelState.Values)
            {
                foreach (var err in item.Errors)
                {
                    error = error + err.ErrorMessage + ".";
                }
            }

            return error;
        }
    }
}