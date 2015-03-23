using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.Data
{
	internal static class StringBuilderExtensions
	{
		public static StringBuilder Indent(this StringBuilder builder, int d)
		{
			while (d > 0)
			{
				builder.Append("\t");
				d--;
			}
			return builder;
		}
		public static StringBuilder Newline(this StringBuilder builder)
		{
			return builder.Append("\r\n");
		}
	}
	public static class StringExtensions
	{
		public static string As(this string s, string alias)
		{
			return s + " AS [" + alias + "]";
		}
		public static string ASC(this string s)
		{
			return s + " ASC";
		}
		public static string DESC(this string s)
		{
			return s + " DESC";
		}
	}
	public static class IEnumerableExtensions
	{
		public static string JoinNonEmpty(this IEnumerable<string> list, string separator)
		{
			if (separator == null) separator = "";

			var result = new StringBuilder();
			var first = true;
			foreach (string text in list)
			{
				// skip null/empty values
				if (string.IsNullOrEmpty(text)) continue;
				// append separator if not the first value in the list
				if (first) first = false; else result.Append(separator);
				// append value
				result.Append(text);
			}
			return result.ToString();
		}
	}

	public class Sequel
	{
		public static bool PrettyPrint;

		StringBuilder _builder;
		bool _prettyPrint;
		int _depth = 1;
		int _paramIndex = 0;
		DynamicParameters _params;

		//public static Sequel Create(bool prettyPrint = false)
		//{
		//	return new Sequel(prettyPrint);
		//}
		public static Sequel NewSelect(bool prettyPrint, params string[] columns)
		{
			return new Sequel(prettyPrint).Select(columns);
		}
		public static Sequel NewSelect(params string[] columns)
		{
			return new Sequel(Sequel.PrettyPrint).Select(columns);
		}

		private Sequel(bool prettyPrint)
		{
			_prettyPrint = prettyPrint;
			_builder = new StringBuilder();
			_params = new DynamicParameters();
		}

		public string Sql
		{
			get { return _builder.ToString(); }
		}
		public DynamicParameters Params
		{
			get { return _params; }
		}

		public Sequel Select(params string[] columns)
		{
			if (_builder.Length > 0)
				if (_prettyPrint) _builder.Newline().Indent(_depth - 1); //else _builder.Append(" ");
			_builder.Append("SELECT");
			if (columns.Length > 0)
				Columns(columns);
			return this;
		}
		public Sequel Columns(params string[] columns)
		{
			return AppendColumns(columns);
		}

		protected Sequel AppendColumns(string[] columns)
		{
			bool first = true;
			foreach (var c in columns)
			{
				if (_prettyPrint) _builder.Newline().Indent(_depth);

				if (first)
				{
					first = false;
					if (!_prettyPrint) _builder.Append(" ");
				}
				else if (_prettyPrint)
					_builder.Append(", ");
				else
					_builder.Append(",");

				_builder.Append(c);
			}
			return this;
		}

		public Sequel Top(string top, bool percent = false)
		{
			if (top == null)
				return this;
			_builder.Append(" TOP(");
			_builder.Append(top);
			_builder.Append(")");
			if (percent) _builder.Append("PERCENT");
			return this;
		}

		public Sequel WithNoLock()
		{
			_builder.Append(" WITH(NOLOCK)");
			return this;
		}

		protected Sequel InternalFrom(string prefix, ITable qt, bool withNoLock)
		{
			if (_prettyPrint) _builder.Newline().Indent(_depth - 1); else _builder.Append(" ");
			_builder.Append(prefix);
			_builder.Append(" ");
			_builder.Append(qt.TableName);
			if (!string.IsNullOrEmpty(qt.Alias))
			{
				_builder.Append(" AS ");
				_builder.Append(qt.Alias);
			}
			if (withNoLock)
				WithNoLock();
			return this;
		}
		public Sequel From(ITable qt, bool withNoLock = false)
		{
			return this.InternalFrom("FROM", qt, withNoLock);
		}
		public Sequel InnerJoin(ITable qt, bool withNoLock = false)
		{
			return this.InternalFrom("INNER JOIN", qt, withNoLock);
		}
		public Sequel LeftOuterJoin(ITable qt, bool withNoLock = false)
		{
			return this.InternalFrom("LEFT OUTER JOIN", qt, withNoLock);
		}

		protected Sequel InternalFrom(string prefix, Action<Sequel> a, string alias)
		{
			if (_prettyPrint) _builder.Newline().Indent(_depth - 1); else _builder.Append(" ");
			_builder.Append(prefix);
			_builder.Append(" (");
			_depth++;
			a(this);
			_depth--;
			if (_prettyPrint) _builder.Newline().Indent(_depth - 1);
			_builder.Append(") AS ");
			_builder.Append(alias);
			return this;
		}
		public Sequel From(Action<Sequel> a, string alias)
		{
			return this.InternalFrom("FROM", a, alias);
		}
		public Sequel InnerJoin(Action<Sequel> a, string alias)
		{
			return this.InternalFrom("INNER JOIN", a, alias);
		}
		public Sequel LeftOuterJoin(Action<Sequel> a, string alias)
		{
			return this.InternalFrom("LEFT OUTER JOIN", a, alias);
		}


		public Sequel On(string column, Comparison comparison, object value, bool literalText = false)
		{
			if (_prettyPrint) _builder.Newline().Indent(_depth - 1); else _builder.Append(" ");
			_builder.Append("ON");
			if (_prettyPrint) _builder.Newline().Indent(_depth); else _builder.Append(" ");
			return this.InternalCompare(column, comparison, value, literalText);
		}
		protected Sequel InternalCompare(string column, Comparison comparison, object value, bool literalText)
		{
			Console.WriteLine("InternalCompare: {0}, {1}", value, value is String);

			_builder.Append("(");
			_builder.Append(column);
			_builder.Append(GetComparisonOperator(comparison));
			if (literalText)
				_builder.Append(value);
			else if (comparison == Comparison.In && (value is System.Collections.IEnumerable))
			{
				_builder.Append("(");
				_depth++;

				bool first = true;
				foreach (var v in (System.Collections.IEnumerable)value)
				{
					if (_prettyPrint) _builder.Newline().Indent(_depth);

					if (first)
						first = false;
					else if (_prettyPrint)
						_builder.Append(", ");
					else
						_builder.Append(",");

					this.NextParam(v);
				}

				_depth--;
				if (_prettyPrint) _builder.Newline().Indent(_depth);
				_builder.Append(")");
			}
			else
				this.NextParam(value);
			_builder.Append(")");
			return this;
		}
		public Sequel Compare(string column, Comparison comparison, object value, bool literalText = false)
		{
			if (_prettyPrint) _builder.Newline().Indent(_depth);
			return this.InternalCompare(column, comparison, value, literalText);
		}

		public Sequel Where(string column, Comparison comparison, object value, bool literalText = false)
		{
			if (_prettyPrint) _builder.Newline().Indent(_depth - 1); else _builder.Append(" ");
			_builder.Append("WHERE");
			if (_prettyPrint) _builder.Newline().Indent(_depth); else _builder.Append(" ");
			return this.InternalCompare(column, comparison, value, literalText);
		}

		public Sequel And(string column, Comparison comparison, object value, bool literalText = false)
		{
			if (_prettyPrint) _builder.Newline().Indent(_depth); else _builder.Append(" ");
			_builder.Append("AND ");
			return this.InternalCompare(column, comparison, value, literalText);
		}
		public Sequel And(Action<Sequel> and)
		{
			if (_prettyPrint) _builder.Newline().Indent(_depth); else _builder.Append(" ");
			_builder.Append("AND (");
			_depth++;
			and(this);
			_depth--;
			if (_prettyPrint) _builder.Newline().Indent(_depth);
			_builder.Append(")");
			return this;
		}
		public Sequel Or(string column, Comparison comparison, object value, bool literalText = false)
		{
			if (_prettyPrint) _builder.Newline().Indent(_depth); else _builder.Append(" ");
			_builder.Append("OR ");
			return this.InternalCompare(column, comparison, value, literalText);
		}

		private void NextParam(object value)
		{
			var name = "@" + (_paramIndex++);
			_builder.Append(name);
			_params.Add(name, value);
		}


		public Sequel Union()
		{
			if (_prettyPrint) _builder.Newline().Indent(_depth - 1); else _builder.Append(" ");
			_builder.Append("UNION");
			if (!_prettyPrint) _builder.Append(" ");
			return this;
		}


		public Sequel GroupBy(params string[] columns)
		{
			if (_prettyPrint) _builder.Newline().Indent(_depth - 1); else _builder.Append(" ");
			_builder.Append("GROUP BY");
			return this.AppendColumns(columns);
		}
		public Sequel OrderBy(params string[] columns)
		{
			if (_prettyPrint) _builder.Newline().Indent(_depth - 1); else _builder.Append(" ");
			_builder.Append("ORDER BY");
			return this.AppendColumns(columns);
		}

		internal static string GetComparisonOperator(Comparison comp)
		{
			string s;
			switch (comp)
			{
				default:
				case Comparison.Equals: s = " = "; break;
				case Comparison.NotEquals: s = " != "; break;
				case Comparison.Like: s = " LIKE "; break;
				case Comparison.NotLike: s = " NOT LIKE "; break;
				case Comparison.GreaterThan: s = " > "; break;
				case Comparison.GreaterOrEquals: s = " >= "; break;
				case Comparison.LessThan: s = " < "; break;
				case Comparison.LessOrEquals: s = " <= "; break;
				case Comparison.Is: s = " IS "; break;
				case Comparison.IsNot: s = " IS NOT "; break;
				case Comparison.In: s = " IN "; break;
				case Comparison.NotIn: s = " NOT IN "; break;
			}
			return s;
		}
	}
	public enum Comparison
	{
		Equals,
		NotEquals,
		Like,
		NotLike,
		GreaterThan,
		GreaterOrEquals,
		LessThan,
		LessOrEquals,
		Is,
		IsNot,
		In,
		NotIn,
		BetweenAnd
	}

	//public class QueryTable
	//{
	//	readonly string _body;
	//	protected readonly string _aliasNoDot;
	//	protected readonly string _alias;

	//	public QueryTable(string databaseName, string schemaName, string tableName, string alias = "")
	//		: this(new List<string>() { databaseName, schemaName, tableName }.JoinNonEmpty("."), alias)
	//	{
	//	}
	//	public QueryTable(string body, string alias = "")
	//	{
	//		_body = body;
	//		_aliasNoDot = alias;
	//		_alias = (!string.IsNullOrEmpty(alias)) ? (alias + ".") : "";
	//	}

	//	public string Body { get { return _body; } }
	//	public string Alias { get { return _aliasNoDot; } }

	//	public string All
	//	{
	//		get { return _alias + "*"; }
	//	}
	//}
}
