// Gorapulv.Api/Controllers/ArticlesController.cs
using Gorapulv.Api.Data;
using Gorapulv.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
[ApiController]
[Route("api/[controller]")]
public class ArticlesController : ControllerBase
{
    private readonly AppDbContext _context;
    public ArticlesController(AppDbContext context)
    {
        _context = context;
    }
    // GET: api/Articles – Récupère tous les articles
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
    {
        var articles = await _context.Articles.ToListAsync();
        return Ok(articles);
    }
    // GET: api/Articles/5 – Récupère un article par id
    [HttpGet("{id}")]
    public async Task<ActionResult<Article>> GetArticle(int id)
    {
        var article = await _context.Articles.FindAsync(id);
        if (article == null)
            return NotFound();
        return Ok(article);
    }
    // POST: api/Articles – Crée un nouvel article
    [HttpPost]
    public async Task<ActionResult<Article>> CreateArticle(Article article)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        article.CreatedAt = DateTime.UtcNow;
        _context.Articles.Add(article);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetArticle), new { id = article.Id },
            article);
    }
    // PUT: api/Articles/5 – Met à jour un article existant
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateArticle(int id, Article
        updatedArticle)
    {
        if (id != updatedArticle.Id)
            return BadRequest("Article ID mismatch");
        _context.Entry(updatedArticle).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!_context.Articles.Any(e
                                                       => e.Id == id))
        {
            return NotFound();
        }
        return NoContent();
    }
    // DELETE: api/Articles/5 – Supprime un article
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteArticle(int id)
    {
        var article = await _context.Articles.FindAsync(id);
        if (article == null)
            return NotFound();
        _context.Articles.Remove(article);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
