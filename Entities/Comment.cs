namespace Gaming.Entities
{
	public class Comment : BaseEntitiy
	{
		public string Text { get; set; }
		public DateTime CreationTime { get; set; }
		public User User { get; set; }
		public GamingShop Gaming { get; set; }
	}
}
