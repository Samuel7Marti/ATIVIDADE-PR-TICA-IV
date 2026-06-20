using System;
using System.Collections.Generic;

namespace PlataformaCursosConsole
{
    public interface IProcessadorPagamento
    {
        void Processar(decimal valor);
    }

    public class RegraNegocioException : Exception
    {
        public RegraNegocioException(string mensagem) : base(mensagem) { }
    }

    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public Aluno(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }

    public class Aula
    {
        public int Id { get; set; }
        public string Titulo { get; set; }

        public Aula(int id, string titulo)
        {
            Id = id;
            Titulo = titulo;
        }
    }

    public abstract class Curso
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public List<Aula> Aulas { get; set; } = new List<Aula>();

        protected Curso(int id, string titulo)
        {
            Id = id;
            Titulo = titulo;
        }

        public void AdicionarAula(Aula aula)
        {
            Aulas.Add(aula);
        }
    }

    public class CursoGratuito : Curso
    {
        public CursoGratuito(int id, string titulo) : base(id, titulo) { }
    }

    public class CursoPago : Curso
    {
        public decimal Preco { get; set; }

        public CursoPago(int id, string titulo, decimal preco) : base(id, titulo)
        {
            Preco = preco;
        }
    }

    public class Matricula
    {
        public Aluno Aluno { get; private set; }
        public Curso Curso { get; private set; }
        public List<Aula> AulasConcluidas { get; private set; } = new List<Aula>();
        public bool Pago { get; private set; }

        public Matricula(Aluno aluno, Curso curso)
        {
            Aluno = aluno;
            Curso = curso;
            Pago = curso is CursoGratuito;
        }

        public void ConfirmarPagamento(IProcessadorPagamento processador)
        {
            if (Curso is CursoPago cursoPago)
            {
                processador.Processar(cursoPago.Preco);
                Pago = true;
            }
        }

        public void ConcluirAula(Aula aula)
        {
            if (!Pago)
                throw new RegraNegocioException($"Não é possível assistir à aula. O curso '{Curso.Titulo}' ainda não foi pago!");

            if (!Curso.Aulas.Contains(aula))
                throw new RegraNegocioException($"A aula '{aula.Titulo}' não pertence ao curso '{Curso.Titulo}'.");

            if (!AulasConcluidas.Contains(aula))
            {
                AulasConcluidas.Add(aula);
            }
        }

        public double ObterPorcentagemProgresso()
        {
            if (Curso.Aulas.Count == 0) return 0;
            return ((double)AulasConcluidas.Count / Curso.Aulas.Count) * 100;
        }
    }

    public class PagamentoPix : IProcessadorPagamento
    {
        public void Processar(decimal valor)
        {
            Console.WriteLine($"[Pagamento] Pix gerado no valor de R$ {valor}. Sucesso!");
        }
    }

    public class PagamentoCartao : IProcessadorPagamento
    {
        public void Processar(decimal valor)
        {
            Console.WriteLine($"[Pagamento] Transação de R$ {valor} aprovada no Cartão!");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== PLATAFORMA DE CURSOS - ALFAUNIPAC ===\n");

            Aluno aluno = new Aluno(1, "Samuel Martins");
            
            Aula aula1 = new Aula(101, "Introdução à Orientação a Objetos");
            Aula aula2 = new Aula(102, "Classes e Objetos na Prática");

            CursoGratuito cursoGit = new CursoGratuito(1, "Git e GitHub Essencial");
            cursoGit.AdicionarAula(new Aula(201, "Criando Repositórios"));

            CursoPago cursoCsharp = new CursoPago(2, "C# Avançado", 149.90m);
            cursoCsharp.AdicionarAula(aula1);
            cursoCsharp.AdicionarAula(aula2);

            // Execução Curso Gratuito
            Console.WriteLine($"Matriculando em: {cursoGit.Titulo}");
            Matricula matGratuita = new Matricula(aluno, cursoGit);
            matGratuita.ConcluirAula(cursoGit.Aulas[0]);
            Console.WriteLine($"Progresso: {matGratuita.ObterPorcentagemProgresso()}%\n");

            // Execução Curso Pago
            Console.WriteLine($"Matriculando em: {cursoCsharp.Titulo}");
            Matricula matPaga = new Matricula(aluno, cursoCsharp);

            try
            {
                matPaga.ConcluirAula(aula1);
            }
            catch (RegraNegocioException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[Bloqueio] {ex.Message}");
                Console.ResetColor();
            }

            Console.WriteLine("\nEfetuando pagamento...");
            matPaga.ConfirmarPagamento(new PagamentoPix());

            Console.WriteLine("\nAssistindo aulas:");
            matPaga.ConcluirAula(aula1);
            Console.WriteLine($"Progresso: {matPaga.ObterPorcentagemProgresso()}%");
            
            matPaga.ConcluirAula(aula2);
            Console.WriteLine($"Progresso: {matPaga.ObterPorcentagemProgresso()}%");

            Console.ReadLine();
        }
    }
}