
namespace Bancomer.Entities.Request
{
	public class PaginationParams
	{
		public PaginationParams() {
			Offset = 0;
			Limit = 10;
		}

		public int Offset { get; set; }

		public int Limit { get; set; }

	}
}


