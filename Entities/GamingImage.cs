namespace Gaming.Entities
{
	public class GamingImage : BaseEntitiy
	{

		public string Path { get; set; }

		public bool? IsMain { get; set; }

		public GamingShop Gaming { get; set; }
	}
}
