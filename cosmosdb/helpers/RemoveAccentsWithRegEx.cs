﻿using System;
using System.Text.RegularExpressions;

namespace tacocat.cosmosdb.helpers
{
    public static class RemoveAccentsWithRegexExtension
    {
		public static string RemoveAccentsWithRegEx(this string inputString)
		{
			inputString = inputString.ToLower();
			Regex replace_a_Accents = new Regex("[á|à|ä|â]", RegexOptions.Compiled);
			Regex replace_e_Accents = new Regex("[é|è|ë|ê]", RegexOptions.Compiled);
			Regex replace_i_Accents = new Regex("[í|ì|ï|î]", RegexOptions.Compiled);
			Regex replace_o_Accents = new Regex("[ó|ò|ö|ô]", RegexOptions.Compiled);
			Regex replace_u_Accents = new Regex("[ú|ù|ü|û]", RegexOptions.Compiled);
			inputString = replace_a_Accents.Replace(inputString, "a");
			inputString = replace_e_Accents.Replace(inputString, "e");
			inputString = replace_i_Accents.Replace(inputString, "i");
			inputString = replace_o_Accents.Replace(inputString, "o");
			inputString = replace_u_Accents.Replace(inputString, "u");
			return inputString.ToUpper();
		}
    }
}
