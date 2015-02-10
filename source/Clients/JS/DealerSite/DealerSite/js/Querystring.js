/**
 * Created with JetBrains WebStorm.
 * User: Andres Sosa
 * Date: 5/4/12
 * Time: 12:24 AM
 * To change this template use File | Settings | File Templates.
 */
// make sure SOS is defined (mostly needed in tests)
window.SOS || (window.SOS = {});
//
((function ()
{
	// private static members
	var root = this, SOS = root.SOS,
		Querystring, decode, decodeComponent, getQuery, parseQuery, getFragment;

	// .ctor
	Querystring = SOS.Querystring = function (query)
	{
		this._query = query;
		this._items = parseQuery(query);
	};


	// public members

	Querystring.prototype.removeValue = function (field)
	{
		delete this._items[field];
	};
	Querystring.prototype.getValue = function (field, type)
	{
		if (!field)
		{
			return;
		}

		field = field.toLowerCase(); // case insensitive

		var name, items = this._items, result;
		for (name in items)
		{
			if (field === name.toLowerCase())
			{
				result = items[name];
				if (type)
				{
					// use first value if there is a list
					if(type !== "array" && _.isArray(result))
					{
						result = result[0];
					}

					switch(type)
					{
						case "array":
							if (result)
							{
								// it could already be an array so check if it's a string
								if (typeof items[name] === "string")
								{
									result = [ result ];
								}
							}
							else
							{
								result = [];
							}
							break;
						case "bool":
						case "boolean":
							if (result)
							{
								result = result.toLowerCase(); // case insensitive
								result = (result === "true" || result === "1" || result === "yes" || result === "on");
							}
							else
							{
								result = false;
							}
							break;
						case "int":
							if (result)
							{
								result = parseInt(result, 10);
							}
							break;
						case "string":
							// use result
							break;
					}
				}
				break;
			}
		}

		return result;
	};


	// private statics

	decode = root.decodeURIComponent;


	// public statics

	decodeComponent = Querystring.decodeComponent = function (s)
	{
		// replace "+" with encoded space, then decode
		return decode((s + "").replace(/\+/g, '%20'));
	};
	getQuery = Querystring.getQuery = function (uri)
	{
		var query;

		pos = uri.indexOf('#');
		if (pos > -1)
		{
			uri = uri.substring(0, pos);
		}

		pos = uri.indexOf('?');
		if (pos > -1)
		{
			// escaping?
			query = uri.substring(pos + 1) || null;
			//uri = uri.substring(0, pos);
		}

		return query;
	};
	parseQuery = Querystring.parseQuery = function (string)
	{
		var items = {},
			splits, length, i, v;

		if (!string)
		{
			return items;
		}

		// replace multiple &&&... with &
		// remove starting and trailing ? and &
		string = string.replace(/&+/g, '&').replace(/^\?*&*|&+$/g, '');

		if (!string)
		{
			return {};
		}

		splits = string.split('&'),
			length = splits.length;

		for (i = 0; i < length; i++)
		{
			v = splits[i].split('=');
			name = decodeComponent(v.shift());
			value = v.length ? decodeComponent(v.join('=')) : null; // when no "=" value is null as per spec

			// make comma separated values into an array
			if (value)
			{
				v = value.split(',');
				if (v.length > 1)
				{
					value = v;
				}
			}

			if (items[name] != null)
			{
				// item already exists, change the value into an array of strings

				// null shouldn't be include in array
				if (value != null)
				{
					if (typeof items[name] === "string")
					{
						items[name] = [items[name]];
					}

					if (typeof value === "string")
					{
						items[name].push(value);
					}
					else
					{
						items[name] = _.union(items[name], value);
					}
				}
			}
			else
			{
				items[name] = value;
			}
		}

		return items;
	};

	getFragment = Querystring.getFragment = function (uri)
	{
		var frag;

		pos = uri.indexOf('#');
		if (pos > -1)
		{
			// escaping?
			frag = uri.substring(pos + 1) || null;
			//uri = uri.substring(0, pos);
		}

		return frag;
	};

	// create instance for current url
	SOS.querystring = new Querystring(getQuery(this.location.href));
	SOS.fragment = new Querystring(getFragment(this.location.href));

}).call(window));
