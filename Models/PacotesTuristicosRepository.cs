using System;
using System.Collections.Generic;
using MySqlConnector;

namespace marketb.Models
{
    public class PacotesTuristicosRepository
    {
        private const string DadosConexao = "Database = b2b; Data Source = localhost; User Id = root";

        public void TestarConexao()
        {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();
            Console.WriteLine("Banco de Dados OK");
            Conexao.Close();
        }
        public void Excluir(PacotesTuristicos pacotes)
        {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();
            string QuerySql = "delete from PacotesTuristicos WHERE Id=@Id";
            MySqlCommand Comando = new MySqlCommand(QuerySql, Conexao);
            Comando.Parameters.AddWithValue("@Id", pacotes.Id);
            Comando.ExecuteNonQuery();
            Conexao.Close();
        }
        public void Alterar(PacotesTuristicos pacotes)
        {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();
            string QuerySql = "update PacotesTuristicos set Nome=@Nome, Estado=@estado,produto=@produto, cv=@cv, Usuario=@Usuario WHERE Id=@Id";
            MySqlCommand Comando = new MySqlCommand(QuerySql, Conexao);
            Comando.Parameters.AddWithValue("@Id", pacotes.Id);
            Comando.Parameters.AddWithValue("@Nome", pacotes.Nome);
            
            Comando.Parameters.AddWithValue("@estado", pacotes.estado);
            Comando.Parameters.AddWithValue("@produto", pacotes.produto);
            Comando.Parameters.AddWithValue("@cv", pacotes.cv);
            Comando.Parameters.AddWithValue("@Usuario", pacotes.Usuario);
            Comando.ExecuteNonQuery();
            Conexao.Close();
        }
        public void Inserir(PacotesTuristicos pacotes)
        {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();
            string QuerySql = "insert into PacotesTuristicos (Nome, estado, produto, cv, Usuario) values (@Nome,  @estado, @produto, @cv, @Usuario)";
            MySqlCommand Comando = new MySqlCommand(QuerySql, Conexao);
            Comando.Parameters.AddWithValue("@Id", pacotes.Id);
            Comando.Parameters.AddWithValue("@Nome", pacotes.Nome);
            
            Comando.Parameters.AddWithValue("@estado", pacotes.estado);
            Comando.Parameters.AddWithValue("@produto", pacotes.produto);
            Comando.Parameters.AddWithValue("@cv", pacotes.cv);
            Comando.Parameters.AddWithValue("@Usuario", pacotes.Usuario);
            Comando.ExecuteNonQuery();
            Conexao.Close();
        }
        public List<PacotesTuristicos> Listar()
        {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();
            string QuerySql = "select * from PacotesTuristicos";
            MySqlCommand Comando = new MySqlCommand(QuerySql, Conexao);
            MySqlDataReader Reader = Comando.ExecuteReader();
            List<PacotesTuristicos> ListaPacotes = new List<PacotesTuristicos>();

            while (Reader.Read())
            {
                PacotesTuristicos pacoteEncontrado = new PacotesTuristicos();
                pacoteEncontrado.Id = Reader.GetInt32("Id");

                if (!Reader.IsDBNull(Reader.GetOrdinal("Nome")))
                    pacoteEncontrado.Nome = Reader.GetString("Nome");

                

                if (!Reader.IsDBNull(Reader.GetOrdinal("estado")))
                    pacoteEncontrado.estado = Reader.GetString("estado");
                
                if (!Reader.IsDBNull(Reader.GetOrdinal("produto")))
                    pacoteEncontrado.produto = Reader.GetString("produto");
                 if (!Reader.IsDBNull(Reader.GetOrdinal("cv")))
                    pacoteEncontrado.cv = Reader.GetString("cv");


                if (!Reader.IsDBNull(Reader.GetOrdinal("Usuario")))
                    pacoteEncontrado.Usuario = Reader.GetInt32("Usuario");

                

                ListaPacotes.Add(pacoteEncontrado);
            }
            Conexao.Close();
            return ListaPacotes;
        }

        public PacotesTuristicos BuscarporId(int Id)
        {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();
            string QuerySql = "select * from PacotesTuristicos WHERE Id=@Id";
            MySqlCommand Comando = new MySqlCommand(QuerySql, Conexao);
            Comando.Parameters.AddWithValue("@Id", Id);
            MySqlDataReader Reader = Comando.ExecuteReader();

            PacotesTuristicos pacoteEncontrado = new PacotesTuristicos();

            if (Reader.Read())
            {
                pacoteEncontrado.Id = Reader.GetInt32("Id");
                if (!Reader.IsDBNull(Reader.GetOrdinal("Nome")))
                    pacoteEncontrado.Nome = Reader.GetString("Nome");
                
                if (!Reader.IsDBNull(Reader.GetOrdinal("estado")))
                    pacoteEncontrado.estado = Reader.GetString("estado");
                if (!Reader.IsDBNull(Reader.GetOrdinal("produto")))
                    pacoteEncontrado.produto = Reader.GetString("produto");
                if (!Reader.IsDBNull(Reader.GetOrdinal("cv")))
                    pacoteEncontrado.cv = Reader.GetString("cv");
                if (!Reader.IsDBNull(Reader.GetOrdinal("Usuario")))
                    pacoteEncontrado.Usuario = Reader.GetInt32("Usuario");

                    
            }
            Conexao.Close();
            return pacoteEncontrado;

        }




    }
}
