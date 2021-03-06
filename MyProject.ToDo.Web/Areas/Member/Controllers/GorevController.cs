﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyProject.ToDo.Business.Interfaces;
using MyProject.ToDo.Entities.Concrete;
using MyProject.ToDo.Web.Areas.Admin.Models;

namespace MyProject.ToDo.Web.Areas.Member.Controllers
{
    [Authorize(Roles = "Member")]
    [Area("Member")]
    public class GorevController : Controller
    {
        private readonly IGorevService _gorevService;
        private readonly UserManager<AppUser> _userManager;
        public GorevController(IGorevService gorevService, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _gorevService = gorevService;
        }
        public async Task<IActionResult> Index(int aktifSayfa = 1)
        {
            TempData["Active"] = "gorev";
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            int toplamSayfa;
            var gorevler = _gorevService.GetirTumTablolarlaTamamlanmayan(out toplamSayfa, user.Id, aktifSayfa);


            ViewBag.ToplamSayfa = toplamSayfa;
            ViewBag.AktifSayfa = aktifSayfa;
            List<GorevListAllViewModel> models = new List<GorevListAllViewModel>();

            foreach (var gorev in gorevler)
            {
                GorevListAllViewModel model = new GorevListAllViewModel();
                model.Id = gorev.Id;
                model.Aciklama = gorev.Aciklama;
                model.Aciliyet = gorev.Aciliyet;
                model.Ad = gorev.Ad;
                model.AppUser = gorev.AppUser;
                model.OlusturulmaTarih = gorev.OlusturulmaTarih;
                model.Raporlar = gorev.Raporlar;
                models.Add(model);
            }
            return View(models);
        }
    }
}
