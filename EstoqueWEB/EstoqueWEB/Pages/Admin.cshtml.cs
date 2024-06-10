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

    // Manipulador de página para deletar usuário
    public async Task<IActionResult> OnPostDeleteUser(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                // Usuário deletado com sucesso
                // Redirecione ou atualize a página conforme necessário
            }
            else
            {
                // Lidar com erros ao excluir usuário, se necessário
            }
        }
        return RedirectToPage();
    }

    // Manipulador de página para deletar item de estoque
    public async Task<IActionResult> OnPostDeleteItem(int itemId)
    {
        var success = await _estoqueService.DeleteEstoqueAsync(itemId);
        if (success)
        {
            // Item de estoque deletado com sucesso
            // Redirecione ou atualize a página conforme necessário
        }
        else
        {
            // Lidar com erros ao excluir item de estoque, se necessário
        }
        return RedirectToPage();
    }
}
