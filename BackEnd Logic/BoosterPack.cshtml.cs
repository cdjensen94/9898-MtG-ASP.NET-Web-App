using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

public class BoosterPackModel : PageModel
{
	private readonly ScryfallService _service;

	public List<string> Cards { get; set; }

	public BoosterPackModel(ScryfallService service)
	{
		_service = service;
	}

	public async Task OnPostAsync()
	{
		Cards = await _service.CreateBoosterPackAsync(_service);
	}
}
