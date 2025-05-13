using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Şube2.HelloMvc.DbContexts;
using Şube2.HelloMvc.Models;
using Şube2.HelloMvc.Models.ViewModels;

namespace Şube2.HelloMvc.Controllers
{
    public class OgrenciController : Controller
    {
        public ViewResult Index()
        {
            dynamic ogr = new Ogrenci();
            // ogr.ad = "Ali";  //runtime da hata alır.
            return View("AnaSayfa");
        }
        // [HttpGet]
        //public ViewResult OgrenciDetay(int id)
        //{
        //    Ogrenci? ogrenci = new Ogrenci();
        //    using (var con = new MyDbContext())
        //    {
        //        var bulunanOgr = con.Ogrenciler.Find(id);
        //        ogrenci.OgrenciId = bulunanOgr.OgrenciId;
        //        ogrenci.Ad = bulunanOgr.Ad;
        //        ogrenci.Soyad = bulunanOgr.Soyad;
        //    }
        //    ViewData["ogr"] = ogrenci;
        //    ViewData["Title"] = ogrenci.Ad + " " + ogrenci.Soyad;
        //    ViewData["MetaDesc"] = ogrenci.Ad + " " + ogrenci.Soyad;
        //    ViewBag.Student = ogrenci;
        //    return View(ogrenci);
        //}
        //[HttpPost]
        //public ViewResult OgrenciDetay(Ogrenci ogr)
        //{
        //    if (ogr != null)
        //    {
        //        using (var con = new MyDbContext())
        //        {
        //            con.Ogrenciler.Update(ogr);
        //            con.SaveChanges();
        //        }
        //    }

        //    return View(OgrenciListe());
        //}

         [HttpGet]
        public IActionResult OgrenciDetay(int id)
        {
            using (var ctx = new MyDbContext())
            {
                var ogr = ctx.Ogrenciler.Find(id);
                 return View(ogr);
            }
       
        }
        [HttpPost]
        //public JsonResult OgrenciDetay([FromBody] Ogrenci ogr)
        //{
        //    using (var ctx = new MyDbContext())
        //    {
        //        ctx.Entry(ogr).State=EntityState.Modified;
        //        ctx.SaveChanges();
        //    }
        //    return Json(new { success = true }); //İstemciye, işlemin başarılı olduğunu bildiren bir JSON döndürür. 
        //}

        //İstemciden gelen JSON verisi, Ogrenci modeline dönüştürülür.

        //Bu ogr nesnesi, veritabanındaki karşılık gelen kayıtla eşleştirilir.

        //Veritabanındaki kayıt güncellenir(EntityState.Modified ile).

        //Değişiklikler veritabanına kaydedilir(SaveChanges() ile).

        // Son olarak, işlem başarılıysa JSON formatında bir yanıt döndürülür.



        [HttpPost]
        public JsonResult OgrenciSil(int id)
        {
            using (var ctx = new MyDbContext())
            {
                var ogr = ctx.Ogrenciler.Find(id);
                if (ogr != null)
                {
                    ctx.Ogrenciler.Remove(ogr);
                    ctx.SaveChanges();
                    return Json(new { success = true });  
                }
                return Json(new { success = false }); 
            }
        }


        public ViewResult OgrenciListe()
        {
            List<Ogrenci> ogrList = null;
            using (var con = new MyDbContext())
            {
                ogrList = con.Ogrenciler.ToList();
            }
            return View(ogrList);
        }

        

        [HttpGet]
        public ViewResult OgrenciEkle()
        {
            return View();
        }

        [HttpPost]
        public JsonResult OgrenciEkle(Ogrenci ogr)
        {
            //bool sonuc = false;: Başta işlem başarısız gibi düşünülür.
            //Sonradan başarıyla kayıt yapılırsa true olacak.
            bool sonuc = false;
            using (var ctx = new MyDbContext())
            {
                try
                {
                    ctx.Ogrenciler.Add(ogr);
                    ctx.SaveChanges();
                    sonuc = true;
                }
                catch
                {
                    sonuc = false;
                }
            }

            return Json(new { success = sonuc });
        }

        public JsonResult OgrenciGuncelleAjax(Ogrenci ogr)
        {
            bool sonuc = false;

            if (ogr != null)
            {
                using (var ctx = new MyDbContext())
                {
                    ctx.Entry(ogr).State = EntityState.Modified;
                    ctx.SaveChanges();
                    sonuc = true;
                }
            }

            return Json(new { success = sonuc });
        }


    }
}


//Controller'dan viewe veri taşıma
// 1-ViewData :Key-Value Pair Collection.Keyler mutlaka tekil olmalıdır.Keyler string,Valuelar objectdir,Type-Safe değildir.
// 2-ViewBag: Arka planda ViewData dictionary'sini kullanır.Keylerin tekil olması önemli ,daha önce viewdatada kullandığın keyi burda kullanamazsın.
//ViewBagler Dynamic yapılardır ve içindeki türler Runtime sırasında tespit edilir.(Valueları kontrol ediyor)

// 3-Model:Intellisense desteği var. 
// 4-Tempdata 
//Generic olmayan collectionların veri güvenliği olmaz.



