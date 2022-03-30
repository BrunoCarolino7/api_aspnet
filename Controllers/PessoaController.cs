using System.Threading.Tasks; // programção assíncrona 
using api.Data; // CRUD selecionar , altrerar , deletar e cadastrar
using Microsoft.AspNetCore.Mvc; // criação das rotas 
using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Controllers
{   
    [Controller]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private DataContext dc;

        // Construtor
        public PessoaController(DataContext context)
        {
            this.dc = context; //pegando a pasta data o DataContext.cs
        }
        
        // Post
        [HttpPost("api")]
        public async Task<ActionResult> cadastrar([FromBody] Pessoa p) // //aasíncrono, realizar tarefas aguardando ordens
        
        {
            dc.pessoa.Add(p);
            await dc.SaveChangesAsync();
            return Created("Objeto pessoa", p);
        }
        
        // Get
        [HttpGet("api")]  //mesmo parãmetro do post, o que muda é o tipo de requisição :) Post/Get
        public async Task<ActionResult> listar()
        {
            var dados = await dc.pessoa.ToListAsync();
            return Ok(dados);
        }

        // Get (id)
        [HttpGet("api/{codigo}")]  // filtar informações pelo ID, buscar determinado objeto
        public Pessoa filtrar(int codigo)
        {
            Pessoa p = dc.pessoa.Find(codigo);
            return p;
        }

        // Put
        [HttpPut("api")]
        public async Task<ActionResult> editar([FromBody] Pessoa p)
        {
            dc.pessoa.Update(p); // o "p" é o objeto vindo de frombody (front end)
            await dc.SaveChangesAsync();
            return Ok(p);
        }

        // Delete
        [HttpDelete("api/{codigo}")]
        public async Task<ActionResult> remover(int codigo)
        {
            Pessoa p = filtrar(codigo);

            if(p == null){
                return NotFound();
            }else{
                dc.pessoa.Remove(p);
                await dc.SaveChangesAsync();
                return Ok();
            }

        }

        // Teste
        [HttpGet("oi")] //get é usado para exibir textos, listar etc...
        public string oi()
        {
            return "Hello World";
        }

    }
}