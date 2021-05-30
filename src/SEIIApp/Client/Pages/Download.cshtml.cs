public class DownloadModel : PageModel
{
    private readonly IWebHostEnvironment _env;

    public DownloadModel(IWebHostEnvironment env)
    {
        _env = env;
    }

    public IActionResult OnGet()
    {
        var filePath = Path.Combine(_env.WebRootPath, "files", "file1.xlsx");

        byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

        return File(fileBytes, "application/force-download", "file1.xlsx");
    }
}