using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_TextureDB : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		TextureDB data = (TextureDB)obj;
		// Add your writer.Write calls here.
		writer.Write(data.id);
		writer.Write(data.cht);
		writer.Write(data.chs);
		writer.Write(data.eng);
		writer.Write(data.kor);
		writer.Write(data.jpn);

	}
	
	public override object Read(ES2Reader reader)
	{
		TextureDB data = new TextureDB();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		TextureDB data = (TextureDB)c;
		// Add your reader.Read calls here to read the data into the object.
		data.id = reader.Read<System.String>();
		data.cht = reader.Read<System.String>();
		data.chs = reader.Read<System.String>();
		data.eng = reader.Read<System.String>();
		data.kor = reader.Read<System.String>();
		data.jpn = reader.Read<System.String>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_TextureDB():base(typeof(TextureDB)){}
}