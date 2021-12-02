using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAlpha.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAlpha.ViewComponents
{
    public class GalleryImageViewComponent : ViewComponent
    {
        private readonly ProjectAlphaContext _context;

        public GalleryImageViewComponent(ProjectAlphaContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int P2kpID)
        {
            var items = await _context.P2kp
                .Include(a => a.Narsum)
                .Include(a => a.ImageP2kp)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.P2kpID == P2kpID);
            return View(items);
        }
    }
}
