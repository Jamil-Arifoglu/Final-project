using Gaming.DAL;

namespace Gaming.Services
{
	public class LayoutServvice
	{

		private readonly GamingDbContext _context;
		private readonly IHttpContextAccessor _accessor;

		public LayoutServvice(GamingDbContext context, IHttpContextAccessor accessor)
		{
			_context = context;
			_accessor = accessor;
		}
	}
}
