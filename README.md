# Atividade Prática IV - Programação Orientada a Objetos II

Este repositório foi criado para a entrega da Atividade Prática IV da disciplina de Programação Orientada a Objetos II. O projeto consiste no desenvolvimento de uma aplicação utilizando a linguagem C# no console.

## 📌 Cenário Escolhido: Cenário 01 – Plataforma de Cursos

O escopo do projeto foi desenvolvido com base no **Cenário 01**, que descreve uma plataforma de cursos com os seguintes requisitos e mapeamentos:

* **Alunos, Cursos, Aulas e Matrículas:** Classes principais que estruturam o domínio do sistema.
* **Múltiplas Matrículas:** Permite que um aluno se matricule em diferentes cursos.
* **Estrutura de Grade:** Associação que garante que um curso possua várias aulas.
* **Registro de Progresso:** Controle dinâmico e seguro da evolução do aluno à medida que conclui as aulas.
* **Especialização de Cursos:** Implementação de cursos **Gratuitos** e **Pagos** através de herança.
* **Meios de Pagamento:** Processamento exclusivo para cursos pagos, com suporte a **Cartão** e **Pix**.

---

## 🛠️ Conceitos de POO Aplicados

O código foi estruturado seguindo boas práticas de arquitetura de software para garantir baixo acoplamento e alta coesão:

1. **Abstração e Herança:** A classe `Curso` atua como uma base abstrata que estende comportamentos para as subclasses `CursoGratuito` e `CursoPago`.
2. **Polimorfismo e Inversão de Dependência (Strategy Pattern):** O processamento de pagamentos foi desacoplado por meio da interface `IProcessadorPagamento`. Isso permite que novos métodos de pagamento (como Pix ou Cartão) sejam adicionados sem a necessidade de alterar as regras de negócio centrais.
3. **Encapsulamento e Validação:** A classe `Matricula` gerencia o estado do progresso. O método `ConcluirAula()` valida se a aula pertence ao curso e se o pagamento foi devidamente processado antes de computar o avanço do aluno.
4. **Tratamento de Exceções:** Uso da classe customizada `RegraNegocioException` para disparar erros controlados e evitar falhas ou comportamentos inconsistentes no fluxo do sistema.

---

## 🚀 Como Executar o Projeto

1. Certifique-se de ter o **SDK do .NET Core / .NET 8** instalado.
2. Clone o repositório:
   ```bash
   git clone [https://github.com/samuel7marti/atividade-pr-tica-iv.git](https://github.com/samuel7marti/atividade-pr-tica-iv.git)
