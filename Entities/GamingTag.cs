namespace Gaming.Entities
{
	public class GamingTag : BaseEntitiy
	{
		public int TagId { get; set; }
		public Tag Tag { get; set; }

		public GamingShop Gaming { get; set; }
	}
}
