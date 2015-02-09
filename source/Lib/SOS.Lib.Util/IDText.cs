namespace SOS.Lib.Util
{
	public class IDText
	{
		public const string DisplayMember = "Text";
		public const string ValueMember = "ID";
	}

	public class IDText<T>
	{
		private T _id;

		public IDText(T id, string text)
		{
			ID = id;
			Text = text;
		}

		public T ID
		{
			get { return _id; }
			set { _id = value; }
		}

		public string Text { get; set; }

		public override bool Equals(object obj)
		{
			var other = obj as IDText<T>;
			if (other == null)
				return false;
			return Equals(_id, other._id);
		}

		public override int GetHashCode()
		{
			return _id.GetHashCode();
		}
	}
}