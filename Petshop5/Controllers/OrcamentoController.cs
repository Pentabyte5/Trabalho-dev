using Microsoft.AspNetCore.Mvc;
using Petshop5.Models;

namespace Petshop5.Controllers
{
    public class OrcamentoController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Calcular(string nomeCliente, string telefone, string nomePet, string raca, string porte, bool incluiBanho, bool incluiTosa, bool incluiHidratacao, string horario)
        {
            // 1. Instancia o Pet (Aplicando a sua classe e encapsulamento)
            Pet pet = new Pet { NomePet = nomePet, Raca = raca, Porte = porte };

            // 2. Instancia o Orçamento (Usando o construtor e polimorfismo para cálculo)
            Orcamento orcamento = new Orcamento(pet)
            {
                IncluiBanho = incluiBanho,
                IncluiTosa = incluiTosa,
                IncluiHidratacao = incluiHidratacao
            };

            decimal valorTotal = orcamento.CalcularValor();

            // 3. INTEGRAÇÃO: Salva automaticamente na lista de Clientes/Pets
            int novoIdCliente = Simulacao.ClientesList.Count + 1;
            Cliente novoCliente = new Cliente(novoIdCliente, nomeCliente, telefone);
            novoCliente.Pets.Add(pet);
            Simulacao.ClientesList.Add(novoCliente);

            // 4. INTEGRAÇÃO: Salva automaticamente na Agenda do Dia
            string servicoDescricao = (incluiBanho ? "Banho " : "") + (incluiTosa ? "Tosa " : "") + (incluiHidratacao ? "Hidratação" : "").Trim();
            if (string.IsNullOrWhiteSpace(servicoDescricao)) servicoDescricao = "Serviço Geral";

            int novoIdAgenda = Simulacao.AgendaList.Count + 1;
            Agendamento novoAgendamento = new Agendamento(novoIdAgenda, nomeCliente, nomePet, servicoDescricao, string.IsNullOrEmpty(horario) ? "08:00" : horario);
            Simulacao.AgendaList.Add(novoAgendamento);

            // Passa os dados para a tela de Resultado
            ViewBag.NomeCliente = nomeCliente;
            ViewBag.NomePet = pet.NomePet;
            ViewBag.ValorTotal = valorTotal;

            return View("Index");
        }
    }
}