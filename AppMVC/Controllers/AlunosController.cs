using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppMVC.Models;

namespace AppMVC.Controllers
{
    [Authorize]
    public class AlunosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        //[OutputCache(Duration = 60)] Ideal para dados imutáveis
        [AllowAnonymous]// Permite que mesmo não estando autorizado para acessar o controller, um usuário anônimo acesse o método GET
        [Route("listar-alunos")]
        public async Task<ActionResult> Index()
        {
            return View(await db.Alunos.ToListAsync());
        }

        [HttpGet]
        [Route("aluno-detalhe/{id:int}")]
        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var aluno = await db.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }

        [HttpGet]
        [Route("novo-aluno")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("novo-aluno")]
        [HandleError(ExceptionType = typeof(NullReferenceException), View = "ViewExceptionHandler")]
        //[ValidateInput(false)] Habilita para validações de dados, mesmo sendo de maneira arriscada ou perigosa
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,name,email,cpf,ativo")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                aluno.data_matricula = DateTime.Now;
                db.Alunos.Add(aluno);
                await db.SaveChangesAsync();

                TempData["Mensagem"] = "Aluno cadastrado com sucesso!";

                return RedirectToAction("Index");
            }

            return View(aluno);
        }

        [HttpGet]
        [Route("editar-aluno/{id:int}")]
        public async Task<ActionResult> Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluno aluno = await db.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }

        [HttpPost]
        [Route("editar-aluno/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,name,email,cpf,data_matricula,descricao,ativo")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aluno).State = EntityState.Modified;
                // Para tornar o campo data_matricula não obriatório
                db.Entry(aluno).Property(a => a.data_matricula).IsModified = false;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(aluno);
        }

        [HttpGet]
        [Route("excluir-aluno/{id:int}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluno aluno = await db.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }

        [HttpPost]
        [Route("excluir-aluno/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Aluno aluno = await db.Alunos.FindAsync(id);
            db.Alunos.Remove(aluno);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
