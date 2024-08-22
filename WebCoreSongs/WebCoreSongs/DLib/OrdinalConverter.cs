namespace WebCoreSongs.DLib
{
	public class OrdinalConverter
	{
		public string OrdinalTerm(long? numNullable)
		{
			if (numNullable == null)
				return "";

			int num = (int)numNullable;
			if (num <= 0) return num.ToString();

			switch (num % 100)
			{
				case 11:
				case 12:
				case 13:
					return num + "th";
			}

			switch (num % 10)
			{
				case 1:
					return num + "st";
				case 2:
					return num + "nd";
				case 3:
					return num + "rd";
				default:
					return num + "th";
			}
		}
	}
}
