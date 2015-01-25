using System;
using System.Reflection;
using UnityEngine;

public class Text {
	public SystemLanguage language = SystemLanguage.English;
	private Type EnglishText = typeof(en);
	private Type SpanishText = typeof(es);

	public Text() {
	}

	public string GetLine(string line) {
		FieldInfo langStringField;

		switch (language) {
			case SystemLanguage.English:
				langStringField = EnglishText.GetField(line);
				return langStringField.GetValue(EnglishText) as string;
			//break;

			case SystemLanguage.Spanish:
				langStringField = SpanishText.GetField(line);
				return langStringField.GetValue(SpanishText) as string;
			//break;

			default:
				langStringField = EnglishText.GetField(line);
				return langStringField.GetValue(EnglishText) as string;
			//break;
		}
	}
}
