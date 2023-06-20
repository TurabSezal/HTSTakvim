using HTSTakvim.App_Classes;
using HTSTakvim.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace HTSTakvim.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            var urunler = Context.Baglanti.Urun.AsQueryable();
            return View(urunler.ToList());
        }

        public ActionResult Olaylar(DateTime? filterDate)
        {
            var urunler = Context.Baglanti.Urun.AsQueryable();

            if (filterDate.HasValue)
            {
                var truncatedFilterDate = filterDate.Value.Date;

                urunler = urunler.Where(u => DbFunctions.TruncateTime(u.IslemZamani) == truncatedFilterDate);
            }

            ViewBag.FilterDate = filterDate;

            return View(urunler.ToList());
        }
        public ActionResult OlaySil(int id)
        {
            var urun = Context.Baglanti.Urun.FirstOrDefault(u => u.Id == id);
            if (urun != null)
            {
                Context.Baglanti.Urun.Remove(urun);
                Context.Baglanti.SaveChanges();
            }
            else
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Olaylar");
        }
        public ActionResult OlayGuncelle(int id)
        {
            ViewBag.Kategoriler = Context.Baglanti.Kategori.ToList();
            var olay = Context.Baglanti.Urun.FirstOrDefault(u => u.Id == id);
            return View(olay);
        }
        [HttpPost]
        public ActionResult OlayGuncelle(Urun urn, int id)
        {
            var olay = Context.Baglanti.Urun.FirstOrDefault(u => u.Id == id);
            if (olay != null)
            {
                olay.IslemZamani = urn.IslemZamani;
                olay.Adi = urn.Adi;
                olay.SatisFiyati = urn.SatisFiyati;
                olay.KategoriID = urn.KategoriID;
                olay.Aciklama = urn.Aciklama;

            }
            Context.Baglanti.SaveChanges();
            return RedirectToAction("Olaylar");
        }
        public ActionResult OlayEkle()
        {
            ViewBag.Kategoriler = Context.Baglanti.Kategori.ToList();
            ViewBag.Markalar = Context.Baglanti.Marka.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult OlayEkle(Urun urn)
        {
            Context.Baglanti.Urun.Add(urn);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("Olaylar");
        }


        public ActionResult OlayTipi()
        {
            return View(Context.Baglanti.Kategori.ToList());
        }

        public ActionResult OlayTipiEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OlayTipiEkle(Kategori ktg)
        {
            Context.Baglanti.Kategori.Add(ktg);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("OlayTipi");
        }
    }
}