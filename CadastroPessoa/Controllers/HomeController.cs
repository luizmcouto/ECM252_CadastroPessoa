using System.Collections.Generic;
using System.Web.Mvc;
using CadastroPessoa.DAO;
using CadastroPessoa.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private PessoaDAO pessoaDAO;

        public HomeController()
        {
            pessoaDAO = new PessoaDAO();
        }

        // GET: Home
        public ActionResult Index()
        {
            List<PessoaDAO> pessoasDAO = pessoaDAO.ListarTodos();
            List<Pessoa> pessoas = new List<Pessoa>();

            foreach (var pessoaDAO in pessoasDAO)
            {
                var pessoa = new Pessoa();

                pessoa.Id = pessoaDAO.Id;
                pessoa.Nome = pessoaDAO.Nome;

                pessoas.Add(pessoa);
            }
            return View(pessoas);
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(Pessoa pessoa)
        {
            pessoaDAO.Id = pessoa.Id;
            pessoaDAO.Nome = pessoa.Nome;

            if (ModelState.IsValid)
            {
                pessoaDAO.Salvar(pessoaDAO);
                return RedirectToAction("Index");
            }
            return View(pessoa);
        }

        public ActionResult Editar(int id)
        {
            pessoaDAO = pessoaDAO.ListarPorId(id);

            if (pessoaDAO == null)
            {
                return HttpNotFound();
            }

            Pessoa pessoa = new Pessoa();

            pessoa.Id = pessoaDAO.Id;
            pessoa.Nome = pessoaDAO.Nome;

            return View(pessoa);
        }

        [HttpPost]
        public ActionResult Editar(Pessoa pessoa)
        {
            pessoaDAO.Id = pessoa.Id;
            pessoaDAO.Nome = pessoa.Nome;

            if (ModelState.IsValid)
            {
                pessoaDAO.Salvar(pessoaDAO);

                return RedirectToAction("Index");
            }
            return View(pessoa);
        }

        public ActionResult Detalhe(int id)
        {
            pessoaDAO = pessoaDAO.ListarPorId(id);

            if (pessoaDAO == null)
            {
                return HttpNotFound();
            }

            Pessoa pessoa = new Pessoa();

            pessoa.Id = pessoaDAO.Id;
            pessoa.Nome = pessoaDAO.Nome;

            return View(pessoa);
        }

        public ActionResult Excluir(int id)
        {
            pessoaDAO = pessoaDAO.ListarPorId(id);

            if (pessoaDAO == null)
            {
                return HttpNotFound();
            }

            Pessoa pessoa = new Pessoa();

            pessoa.Id = pessoaDAO.Id;
            pessoa.Nome = pessoaDAO.Nome;

            return View(pessoa);
        }

        [HttpPost, ActionName("Excluir")]
        public ActionResult ConfirmarExcluir(int id)
        {
            pessoaDAO.Excluir(id);

            return RedirectToAction("Index");
        }
    }
}