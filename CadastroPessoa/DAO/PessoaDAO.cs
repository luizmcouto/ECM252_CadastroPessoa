using System.Collections.Generic;
using System.Linq;
using CadastroPessoa.Repositorio;

namespace CadastroPessoa.DAO
{
    public class PessoaDAO
    {
        private readonly Contexto contexto;

        public int Id { get; set; }
        public string Nome { get; set; }

        public PessoaDAO()
        {
            contexto = new Contexto();
        }

        public List<PessoaDAO> ListarTodos()
        {
            var pessoas = new List<PessoaDAO>();
            const string strQuery = "SELECT Id, Nome FROM Pessoa";

            var rows = contexto.ExecutaComandoComRetorno(strQuery);
            foreach (var row in rows)
            {
                var tempPessoa = new PessoaDAO
                {
                    Id = int.Parse(!string.IsNullOrEmpty(row["Id"]) ? row["Id"] : "0"),
                    Nome = row["Nome"]
                };
                pessoas.Add(tempPessoa);
            }

            return pessoas;
        }

        private int Inserir(PessoaDAO pessoa)
        {
            const string commandText = " INSERT INTO Pessoa (Nome) VALUES (@Nome) ";

            var parameters = new Dictionary<string, object>
            {
                {"Nome", pessoa.Nome}
            };

            return contexto.ExecutaComando(commandText, parameters);
        }

        private int Alterar(PessoaDAO pessoa)
        {
            var commandText = " UPDATE Pessoa SET ";
            commandText += " Nome = @Nome ";
            commandText += " WHERE Id = @Id ";

            var parameters = new Dictionary<string, object>
            {
                {"Id", pessoa.Id},
                {"Nome", pessoa.Nome}
            };

            return contexto.ExecutaComando(commandText, parameters);
        }

        public void Salvar(PessoaDAO pessoa)
        {
            if (pessoa.Id > 0)
                Alterar(pessoa);
            else
                Inserir(pessoa);
        }

        public int Excluir(int id)
        {
            const string strQuery = "DELETE FROM Pessoa WHERE Id = @Id";
            var parametros = new Dictionary<string, object>
            {
                {"Id", id}
            };

            return contexto.ExecutaComando(strQuery, parametros);
        }

        public PessoaDAO ListarPorId(int id)
        {
            var pessoas = new List<PessoaDAO>();
            const string strQuery = "SELECT Id, Nome FROM Pessoa WHERE Id = @Id";
            var parametros = new Dictionary<string, object>
            {
                {"Id", id}
            };
            var rows = contexto.ExecutaComandoComRetorno(strQuery, parametros);
            foreach (var row in rows)
            {
                var tempPessoa = new PessoaDAO
                {
                    Id = int.Parse(!string.IsNullOrEmpty(row["Id"]) ? row["Id"] : "0"),
                    Nome = row["Nome"]
                };
                pessoas.Add(tempPessoa);
            }

            return pessoas.FirstOrDefault();
        }
    }
}