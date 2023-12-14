using AspNetCore;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoMusica.Data;
using ProyectoMusica.Models;
using ProyectoMusica.ViewModels;
using System.Diagnostics;
using X.PagedList;
using X.PagedList.Web.Common;

namespace ProyectoMusica.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _ctx;
        private readonly INotyfService _notyf;
        private readonly IConfiguration _conf;

        public HomeController(ApplicationDbContext ctx,INotyfService notyf,IConfiguration conf)
        {
            _ctx = ctx;
            _notyf=notyf;
            _conf = conf;
        }

        [HttpGet]
        public async Task<IActionResult> Index(ListadoGruposViewModel vm)
        {
            var numRegistros = _conf.GetValue("RegistrosPorPagina",5);
            var grupos = _ctx.Grupos;
            var total = grupos.Count();
            vm.Total = total;
            var pagina = vm.Pagina ?? 1;
            vm.ListaGrupos= await grupos.ToPagedListAsync(pagina, numRegistros);
            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task< IActionResult> Create(Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                var existe=await _ctx.Grupos.AnyAsync(g=>g.Nombre.ToLower().Trim()==grupo.Nombre.ToLower().Trim());
                if (existe)
                {
                    //ModelState.AddModelError("","El nombre del grupo " + grupo.Nombre + " ya existe en la Base de Datos");
                    _notyf.Warning("El nombre del grupo " + grupo.Nombre + " ya existe en la Base de Datos");
                    return View(grupo);
                }
                _ctx.Grupos.Add(grupo);
                await _ctx.SaveChangesAsync();
                _notyf.Success("Grupo " + grupo.Nombre + " creado correctamente");
                return RedirectToAction(nameof(Index));

            }
            return View(grupo);
        }


        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            if (Id==null)
            {
                return NotFound();
            }
            var grupo=_ctx.Grupos.Find(Id);
            if (grupo!=null)
            {
                return View(grupo);
            }
            return NotFound();
           
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                _ctx.Grupos.Update(grupo);
                await _ctx.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grupo);
        }


        [HttpGet]
        public IActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var grupo = _ctx.Grupos.Find(Id);
            if (grupo != null)
            {
                return View(grupo);
            }
            return NotFound();

        }

        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var grupo = _ctx.Grupos.Find(Id);
            if (grupo != null)
            {
                return View(grupo);
            }
            return NotFound();

        }


        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteGrupo(int Id)
        {
         
            var grupo = _ctx.Grupos.Find(Id);
            if (grupo != null)
            {
               _ctx.Grupos.Remove(grupo);
                await _ctx.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(grupo);

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}