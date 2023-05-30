namespace Gaming.Entities
{
	public class Tag : BaseEntitiy
	{

		public string Name { get; set; }

		public List<GamingTag> GamingTag { get; set; }

		public Tag()
		{
			GamingTag = new();
		}
	}
}
