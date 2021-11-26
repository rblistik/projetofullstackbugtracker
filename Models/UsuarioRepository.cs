using System.Collections.Generic;
using MySqlConnector;
using System;

namespace marketb.Models
{
    public class UsuarioRepository
    {
        
        //ter as credenciais de acesso ao banco de dados
        private const string DadosConexao = "Database=b2b;Data Source=localhost;User Id=root";

        //operacoes de manipulação de registros da tabela Usuario
        //CRUD - Inserir(insert), Listar(select), Alterar(update), Excluir(delete)    

        public void TestarConexao(){

            //informar a credencial de acesso
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            
            //abrir uma conexao
            Conexao.Open();
            
            //imprimir uma mensagem de tudo funcionando
            Console.WriteLine("Banco de dados funcionando!");

            //fechar uma conexao    
            Conexao.Close();

        }

        public Usuario ValidarLogin(Usuario user){

            //abrir a conexao com o banco de dados
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open(); 

            //query em sql para buscar por Login e Senha
            String QuerySql = "select * from Usuario WHERE Login=@Login and Senha=@Senha";

            //preparar um comando, passando: sql + conexao com o banco de dados
            MySqlCommand Comando = new MySqlCommand(QuerySql,Conexao); 

            //tratamento devido ao sql injection
            Comando.Parameters.AddWithValue("@Login",user.Login);            
            Comando.Parameters.AddWithValue("@Senha",user.Senha);            

            //executo no banco de dados, que retorna um unico usuario QUANDO encontrado
            MySqlDataReader Reader =  Comando.ExecuteReader();

            //iremos inicializar o obj UsuarioEncontrado com null
            //pq esta estrategia? Caso o obj Reader(49) não encontrar registros
            //o objeto retornara null(77), caso encontre (57) retorna o conjunto de dados
            Usuario UsuarioEncontrado = null;

            //aqui esta a validacao do obj Reader
            if (Reader.Read()) {
                
                //se entrar aqui, siggnifica que encontrou o usuario com login e senha infomados
                UsuarioEncontrado = new Usuario();
                UsuarioEncontrado.Id = Reader.GetInt32("Id");
                
                if (!Reader.IsDBNull(Reader.GetOrdinal("Nome")))
                    UsuarioEncontrado.Nome = Reader.GetString("Nome");
                if (!Reader.IsDBNull(Reader.GetOrdinal("Login")))
                    UsuarioEncontrado.Login = Reader.GetString("Login");
                if (!Reader.IsDBNull(Reader.GetOrdinal("Senha")))
                    UsuarioEncontrado.Senha = Reader.GetString("Senha");
                
                UsuarioEncontrado.DataNascimento = Reader.GetDateTime("DataNascimento");
            }

            //fechar a conexao com o banco
            Conexao.Close();

            //retornar o UsuarioEncontrado
            return UsuarioEncontrado;
        }


        public Usuario BuscarPorId(int Id){

            //abrir a conexao com o banco de dados
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open(); 

            //query em sql para buscar por ID (select)
            String QuerySql = "select * from Usuario WHERE Id=@Id";

            //preparar um comando, passando: sql + conexao com o banco de dados
            MySqlCommand Comando = new MySqlCommand(QuerySql,Conexao); 

            //tratamento devido ao sql injection
            Comando.Parameters.AddWithValue("@Id",Id);            

            //executo no banco de dados, que retorna um unico usuario QUANDO encontrado
            MySqlDataReader Reader =  Comando.ExecuteReader();

            Usuario UsuarioEncontrado = new Usuario();

            if (Reader.Read()) {
                
                UsuarioEncontrado.Id = Reader.GetInt32("Id");
                
                if (!Reader.IsDBNull(Reader.GetOrdinal("Nome")))
                    UsuarioEncontrado.Nome = Reader.GetString("Nome");
                if (!Reader.IsDBNull(Reader.GetOrdinal("Login")))
                    UsuarioEncontrado.Login = Reader.GetString("Login");
                if (!Reader.IsDBNull(Reader.GetOrdinal("Senha")))
                    UsuarioEncontrado.Senha = Reader.GetString("Senha");
                
                UsuarioEncontrado.DataNascimento = Reader.GetDateTime("DataNascimento");
            }

            //fechar a conexao com o banco
            Conexao.Close();

            //retornar o UsuarioEncontrado
            return UsuarioEncontrado;

        }

        public List<Usuario> Listar(){

            //abrir a conexao com o banco de dados
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open(); 

            //query em sql para listagem (select)
            String QuerySql = "select * from Usuario";

            //preparar um comando, passando: sql + conexao com o banco de dados
            MySqlCommand Comando = new MySqlCommand(QuerySql,Conexao); 

            //executo no banco de dados, que retorna uma lista de dados
            MySqlDataReader Reader =  Comando.ExecuteReader();

            //simplesmente criando uma lista de usuario
            List<Usuario> Lista = new List<Usuario>();

            //percorre todos os registros retornados no banco de dados(obj. Reader)
            while(Reader.Read()){

                Usuario userEncontrado = new Usuario();

                userEncontrado.Id = Reader.GetInt32("Id");

                if (!Reader.IsDBNull(Reader.GetOrdinal("Nome")))
                    userEncontrado.Nome = Reader.GetString("Nome");
                
                if (!Reader.IsDBNull(Reader.GetOrdinal("Login")))
                    userEncontrado.Login = Reader.GetString("Login");

                if (!Reader.IsDBNull(Reader.GetOrdinal("Senha")))    
                    userEncontrado.Senha = Reader.GetString("Senha");

                userEncontrado.DataNascimento = Reader.GetDateTime("DataNascimento"); 

                //add na lista de usuarios
                Lista.Add(userEncontrado);
            }

            //fechar a conexao com o banco
            Conexao.Close();

            //retornamos a lista com todos os registros armazenados no banco de dados
            return Lista;

        }

        public void Inserir(Usuario user){

            //abrir a conexao com o banco de dados
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open(); 

            //query em sql para alterar (insert)
            String QuerySql = "insert into Usuario (Nome,Login,Senha,DataNascimento) values (@Nome,@Login,@Senha,@DataNascimento)";

            //preparar um comando, passando: sql + conexao com o banco de dados
            MySqlCommand Comando = new MySqlCommand(QuerySql,Conexao); 

            //tratamento devido ao sql injection
            Comando.Parameters.AddWithValue("@Nome",user.Nome);
            Comando.Parameters.AddWithValue("@Login",user.Login);
            Comando.Parameters.AddWithValue("@Senha",user.Senha);
            Comando.Parameters.AddWithValue("@DataNascimento",user.DataNascimento);

            //executar o comando no banco de dados
            Comando.ExecuteNonQuery();

            //fechar a conexao com o banco
            Conexao.Close();
        }

        public void Alterar(Usuario user){

            //abrir a conexao com o banco de dados
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open(); 

            //query em sql para alterar (update)
            String QuerySql = "update Usuario set Nome=@Nome, Login=@Login, Senha=@Senha, DataNascimento=@DataNascimento WHERE Id=@Id";

            //preparar um comando, passando: sql + conexao com o banco de dados
            MySqlCommand Comando = new MySqlCommand(QuerySql,Conexao); 

            //tratamento devido ao sql injection
            Comando.Parameters.AddWithValue("@Id",user.Id);
            Comando.Parameters.AddWithValue("@Nome",user.Nome);
            Comando.Parameters.AddWithValue("@Login",user.Login);
            Comando.Parameters.AddWithValue("@Senha",user.Senha);
            Comando.Parameters.AddWithValue("@DataNascimento",user.DataNascimento);

            //executar o comando no banco de dados
            Comando.ExecuteNonQuery();

            //fechar a conexao com o banco
            Conexao.Close();
        }

        public void Excluir(Usuario user){

            //abrir a conexao com o banco de dados
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open(); 

            //query em sql para excluir (delete)
            String QuerySql = "delete from Usuario WHERE Id=@Id";

            //preparar um comando, passando: sql + conexao com o banco de dados
            MySqlCommand Comando = new MySqlCommand(QuerySql,Conexao); 

            //tratamento devido ao sql injection
            Comando.Parameters.AddWithValue("@Id",user.Id);

            //executar o comando no banco de dados
            Comando.ExecuteNonQuery();

            //fechar a conexao com o banco
            Conexao.Close();
        }

    }
}