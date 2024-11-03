using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var searchDb = _context.Tarefas.Find(id);

            if (searchDb == null)
                return NotFound();

            return Ok(searchDb);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            var searchAllDb = _context.Tarefas.ToList();
            return Ok(searchAllDb);
        }

        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o titulo recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            var searchAllTitleDb = _context.Tarefas.Where(t => t.Titulo == titulo);
            return Ok(searchAllTitleDb);
        }

        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            var searchAllTaskDb = _context.Tarefas.Where(x => x.Data.Date == data.Date);
            return Ok(searchAllTaskDb);
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            var tarefa = _context.Tarefas.Where(x => x.Status == status);
            return Ok(tarefa);
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // TODO: Adicionar a tarefa recebida no EF e salvar as mudanças (save changes)
            var createDb = _context.Tarefas.Add(tarefa);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var taskDb = _context.Tarefas.Find(id);

            if (taskDb == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // TODO: Atualizar as informações da variável tarefaBanco com a tarefa recebida via parâmetro
            // TODO: Atualizar a variável tarefaBanco no EF e salvar as mudanças (save changes)
            taskDb.Titulo = tarefa.Titulo;
            taskDb.Descricao = tarefa.Descricao;
            taskDb.Data = tarefa.Data;
            taskDb.Status = tarefa.Status;

            _context.Update(taskDb);
            _context.SaveChanges();
            return Ok(taskDb);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var taskDb = _context.Tarefas.Find(id);

            if (taskDb == null)
                return NotFound();

            // TODO: Remover a tarefa encontrada através do EF e salvar as mudanças (save changes)
            _context.Remove(taskDb);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
