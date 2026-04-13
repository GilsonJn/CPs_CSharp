using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP1_Cadastro
{
    internal class Cadastro<T>
    {
        private Dictionary<int, T> pessoas = new Dictionary<int, T>();

        public bool Adicionar(int id, T pessoa)
        {
            if (pessoas.ContainsKey(id))
            {
                return false; // ID já existe
            }

            pessoas.Add(id, pessoa);
            return true;
        }

        public Dictionary<int, T> ListarTodos()
        {
            return pessoas;
        }

        public T Buscar(int id)
        {
            if (pessoas.TryGetValue(id, out T pessoa))
            {
                return pessoa;
            }
            return default; // Retorna o valor padrão se não encontrado
        }

        public bool Remover(int id)
        {
            return pessoas.Remove(id);
        }
    }
}
