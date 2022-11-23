using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_LanguageDB : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		LanguageDB data = (LanguageDB)obj;
		// Add your writer.Write calls here.
		writer.Write(data.key);
		writer.Write(data.cht);
		writer.Write(data.chs);
		writer.Write(data.eng);
		writer.Write(data.kor);
		writer.Write(data.jpn);

	}
	
	public override object Read(ES2Reader reader)
	{
		LanguageDB data = new LanguageDB();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		LanguageDB data = (LanguageDB)c;
		// Add your reader.Read calls here to read the data into the object.
		data.key = reader.Read<System.String>();
		data.cht = reader.Read<System.String>();
		data.chs = reader.Read<System.String>();
		data.eng = reader.Read<System.String>();
		data.kor = reader.Read<System.String>();
		data.jpn = reader.Read<System.String>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_LanguageDB():base(typeof(LanguageDB)){}
}