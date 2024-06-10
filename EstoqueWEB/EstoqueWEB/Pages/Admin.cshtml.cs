using EstoqueWEB.Interface.Service;
using EstoqueWEB.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

public class AdminModel : PageModel
{
    private readonly UserManager<AplicationUser> _userManager;
    private readonly IEstoqueService _estoqueService;

    public AdminModel(UserManager<AplicationUser> userManager, IEstoqueService estoqueService)
    {
        _userManager = userManager;
        _estoqueService = estoqueService;
    }

    // Manipulador de p�gina para deletar usu�rio
    public async Task<IActionResult> OnPostDeleteUser(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                // Usu�rio deletado com sucesso
                // Redirecione ou atualize a p�gina conforme necess�rio
            }
            else
            {
                // Lidar com erros ao excluir usu�rio, se necess�rio
            }
        }
        return RedirectToPage();
    }

    // Manipulador de p�gina para deletar item de estoque
    public async Task<IActionResult> OnPostDeleteItem(int itemId)
    {
        var success = await _estoqueService.DeleteEstoqueAsync(itemId);
        if (success)
        {
            // Item de estoque deletado com sucesso
            // Redirecione ou atualize a p�gina conforme necess�rio
        }
        else
        {
            // Lidar com erros ao excluir item de estoque, se necess�rio
        }
        return RedirectToPage();
    }
}
